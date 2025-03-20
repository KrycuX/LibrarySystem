using LibrarySystem.Shared.Wrappers;
using MediatR;

namespace LibrarySystem.Shared.Books.Commands;

public class DeleteBookCommand : IRequest<ResponseResult>
{
	public Guid Id { get; init; }
}
