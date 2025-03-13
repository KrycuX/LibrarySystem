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

public class CreateBookHandlerTests
{
	private readonly Mock<IBookRepository> _bookRepositoryMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly CreateBookHandler _handler;

	public CreateBookHandlerTests()
	{
		_bookRepositoryMock = new Mock<IBookRepository>();
		_mapperMock = new Mock<IMapper>();
		_handler = new CreateBookHandler(_bookRepositoryMock.Object, _mapperMock.Object);
	}

	[Fact]
	public async Task Handle_ShouldReturnValidationError_WhenIsbnAlreadyExists()
	{
		// Arrange
		var existingIsbn = "123456";

		_bookRepositoryMock
			.Setup(repo => repo.IsbnExist(existingIsbn))
			.ReturnsAsync(true);

		var command = new CreateBookCommand
		{
			Title = "Test Book",
			Author = "Author Name",
			ISBN = existingIsbn,
			ShelfLocation = "A1"
		};

		// Act
		Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

		// Assert
		await act.Should()
			.ThrowAsync<ValidationException>()
			.Where(ex => ex.Errors.Any(e => e.PropertyName == "ISBN" && e.ErrorMessage == "ISBN must be unique"));

		_bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Never);
	}

	[Fact]
	public async Task Handle_ShouldCreateBook_WhenIsbnIsUnique()
	{
		// Arrange
		var book = new Book { Id = Guid.NewGuid(), Title = "New Book", Author = "New Author", ISBN = "123456" };
		var bookDto = new BookDto { Id = book.Id, Title = book.Title, Author = book.Author, ISBN = book.ISBN };

		_bookRepositoryMock.Setup(repo => repo.IsbnExist("123456")).ReturnsAsync(false);
		_bookRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Book>())).ReturnsAsync(book);
		_mapperMock.Setup(m => m.Map<BookDto>(It.IsAny<Book>())).Returns(bookDto);

		var command = new CreateBookCommand
		{
			Title = "New Book",
			Author = "New Author",
			ISBN = "123456",
			ShelfLocation = "A1"
		};

		// Act
		var result = await _handler.Handle(command, CancellationToken.None);

		// Assert
		result.IsSucceed.Should().BeTrue();
		result.Model.Should().NotBeNull();
		result.Model!.Title.Should().Be("New Book");

		_bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
		_mapperMock.Verify(m => m.Map<BookDto>(It.IsAny<Book>()), Times.Once);
	}
}
