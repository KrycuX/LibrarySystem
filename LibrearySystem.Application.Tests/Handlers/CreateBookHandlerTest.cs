using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using LibrarySystem.Application.Books.Commands;
using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Domain.Entities;
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
    public async Task Handle_ShouldReturnError_WhenIsbnAlreadyExists()
    {
        // Arrange
        _bookRepositoryMock.Setup(repo => repo.CheckIsbnAsync("123456")).ReturnsAsync(true);

        var command = new CreateBookCommand
        {
            Title = "Test Book",
            Author = "Author Name",
            ISBN = "123456",
            ShelfLocation = "A1"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSucceed.Should().BeFalse();
        result.Message.Should().Be("ISBN must be unique");
        result.Model.Should().BeNull();
        _bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldCreateBook_WhenIsbnIsUnique()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), Title = "New Book", Author = "New Author", ISBN = "123456" };
        var bookDto = new BookDto { Id = book.Id, Title = book.Title, Author = book.Author, ISBN = book.ISBN };

        _bookRepositoryMock.Setup(repo => repo.CheckIsbnAsync("123456")).ReturnsAsync(false);
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
