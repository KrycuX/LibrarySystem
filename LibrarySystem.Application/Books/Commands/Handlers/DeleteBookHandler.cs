using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.Books.Commands;
using LibrarySystem.Shared.Wrappers;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class DeleteBookHandler(IBookRepository bookRepository) : IRequestHandler<DeleteBookCommand, ResponseResult>
{
	private readonly IBookRepository _bookRepository = bookRepository;

	public async Task<ResponseResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
	{
		await _bookRepository.DeleteAsync(request.Id);
		return new(true, string.Empty);
	}
}
