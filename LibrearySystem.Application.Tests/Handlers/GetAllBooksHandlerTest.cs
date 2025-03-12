using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using LibrarySystem.Application.Books.Queries;
using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.Books.Query;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Wrappers;
using Moq;

namespace LibrearySystem.Application.Tests.Handlers;

public class GetAllBooksHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly GetAllBooksHandler _handler;

    public GetAllBooksHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new GetAllBooksHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedResult_WhenCalled()
    {
        // Arrange
        var books = new List<BookDto>
        {
            new() { Id = Guid.NewGuid(), Title = "Book 1", Author = "Author 1" },
            new() { Id = Guid.NewGuid(), Title = "Book 2", Author = "Author 2" }
        };

        var paginatedBooks = new PaginatedResult<BookDto>(books, 1, 10, 2,null,false);

        _bookRepositoryMock
            .Setup(repo => repo.GetItemsAsync(1, 10, null, false))
            .ReturnsAsync(paginatedBooks);

        var query = new GetAllBooksQuery { Page = 1, PageSize = 10 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSucceed.Should().BeTrue();
        result.Model.Should().NotBeNull();
        result.Model.Items.Should().HaveCount(2);
        result.Model.Items.Should().ContainSingle(b => b.Title == "Book 1");
        result.Model.Items.Should().ContainSingle(b => b.Title == "Book 2");

        _bookRepositoryMock.Verify(repo => repo.GetItemsAsync(1, 10, null, false), Times.Once);
    }
}
