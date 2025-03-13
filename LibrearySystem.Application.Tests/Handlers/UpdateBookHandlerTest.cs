using FluentAssertions;
using LibrarySystem.Application.Books.Commands;
using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Shared;
using LibrarySystem.Shared.Books.Commands;
using LibrarySystem.Shared.DTOs;
using MapsterMapper;
using Moq;

namespace LibrearySystem.Application.Tests.Handlers;

public class UpdateBookHandlerTests
{
	private readonly Mock<IBookRepository> _bookRepositoryMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly UpdateBookHandler _handler;

	public UpdateBookHandlerTests()
	{
		_bookRepositoryMock = new Mock<IBookRepository>();
		_mapperMock = new Mock<IMapper>();
		_handler = new UpdateBookHandler(_bookRepositoryMock.Object);
	}

	[Fact]
	public async Task Handle_ShouldReturnValidationError_WhenChangingIsbnToExistingOne()
	{
		// Arrange
		var bookId = Guid.NewGuid();
		var existingIsbn = "111111";
		var newIsbn = "222222"; // ISBN, który już istnieje w bazie

		var existingBook = new Book
		{
			Id = bookId,
			Title = "Original Title",
			Author = "Original Author",
			ISBN = existingIsbn,
			ShelfLocation = "A1"
		};

		_bookRepositoryMock
			.Setup(repo => repo.GetByIdAsync(bookId))
			.ReturnsAsync(existingBook); // Pobieramy książkę do edycji

		_bookRepositoryMock
			.Setup(repo => repo.IsbnExist(newIsbn))
			.ReturnsAsync(true); // Nowy ISBN już istnieje

		var command = new UpdateBookCommand
		{
			Id = bookId,
			Title = "Updated Title",
			Author = "Updated Author",
			ISBN = newIsbn, // Próbujemy zmienić na zajęty ISBN
			ShelfLocation = "B2"
		};

		// Act
		Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

		// Assert
		await act.Should()
			.ThrowAsync<ValidationException>()
			.Where(ex => ex.Errors.Any(e => e.PropertyName == "ISBN" && e.ErrorMessage == "ISBN must be unique"));

		_bookRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Never);
	}

}
