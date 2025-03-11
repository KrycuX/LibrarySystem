using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class ShelveBookCommand: IRequest
{

    public Guid Id { get; init; }
    public string ShelfLocation { get; init; } = string.Empty;
}
