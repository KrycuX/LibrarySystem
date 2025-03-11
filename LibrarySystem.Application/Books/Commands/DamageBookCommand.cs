using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class DamageBookCommand: IRequest
{
    public Guid Id { get; init; }
}
