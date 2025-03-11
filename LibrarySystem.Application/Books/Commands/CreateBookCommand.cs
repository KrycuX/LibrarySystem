using LibrarySystem.Shared.DTOs;
using MediatR;


namespace LibrarySystem.Application.Books.Commands;

public class CreateBookCommand : IRequest<BookDto>
{
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string ISBN { get; init; } = string.Empty;
    public string ShelfLocation { get; init; } = string.Empty;
}
