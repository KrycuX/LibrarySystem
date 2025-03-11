using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class UpdateBookCommand : IRequest
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string ISBN { get; init; } = string.Empty;
    public string ShelfLocation { get; init; } = string.Empty;
}
