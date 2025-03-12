using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using FluentAssertions;
using LibrarySystem.Application.Books.Commands;
using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Shared.Books.Commands;
using Moq;

namespace LibrearySystem.Application.Tests.Handlers;

public class BorrowBookHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly BorrowBookHandler _handler;

    public BorrowBookHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new BorrowBookHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenBookDoesNotExist()
    {
        // Arrange
        _bookRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Book?)null);

        var command = new BorrowBookCommand { Id = Guid.NewGuid(), BorrowedBy = "User123" };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSucceed.Should().BeFalse();
        result.Message.Should().Contain($"Book of id: '{command.Id}' do not exist.");
        _bookRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id), Times.Once);
        _bookRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldBorrowBook_WhenBookExists()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), Title = "Test Book" };
        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(book.Id)).ReturnsAsync(book);
        _bookRepositoryMock.Setup(repo => repo.UpdateAsync(book)).Returns(Task.CompletedTask);

        var command = new BorrowBookCommand { Id = book.Id, BorrowedBy = "User123" };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSucceed.Should().BeTrue();
        _bookRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Book>(b => b == book)), Times.Once);
    }
}
