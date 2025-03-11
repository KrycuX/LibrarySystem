using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class DeleteBookCommand: IRequest
{
    public Guid Id { get; init; }
}
