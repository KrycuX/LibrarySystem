using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.Books.Commands;
using LibrarySystem.Shared.Wrappers;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class BorrowBookHandler(IBookRepository bookRepository) : IRequestHandler<BorrowBookCommand, ResponseResult>
{
	private readonly IBookRepository _bookRepository = bookRepository;

	public async Task<ResponseResult> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
		var book = await _bookRepository.GetByIdAsync(request.Id);
		if (book == null)
			return new (false,$"Book of id: '{request.Id}' do not exist.");

		book.Borrow(request.BorrowedBy);
		await _bookRepository.UpdateAsync(book);
		return new(true,string.Empty);
	}
}
