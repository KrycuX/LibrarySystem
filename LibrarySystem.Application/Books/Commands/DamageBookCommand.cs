using LibrarySystem.Shared.Wrappers;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class DamageBookCommand: IRequest<ResponseResult>
{
    public Guid Id { get; init; }
}
