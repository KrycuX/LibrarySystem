using LibrarySystem.Shared.Wrappers;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class ShelveBookCommand: IRequest<ResponseResult>
{

    public Guid Id { get; init; }
    public string ShelfLocation { get; init; } = string.Empty;
}
