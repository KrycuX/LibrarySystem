using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.Books.Commands;
using LibrarySystem.Shared.Wrappers;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class ReturnBookHandler(IBookRepository bookRepository) : IRequestHandler<ReturnBookCommand, ResponseResult>
{
	private readonly IBookRepository _bookRepository = bookRepository;

	public async Task<ResponseResult> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
		var book = await _bookRepository.GetByIdAsync(request.Id);
		if (book == null)
			return new(false, $"Book of id: '{request.Id}' do not exist.");

		book.Return();
		await _bookRepository.UpdateAsync(book);
		return new(true, string.Empty);
	}
}
