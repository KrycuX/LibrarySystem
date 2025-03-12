using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.Books.Commands;
using LibrarySystem.Shared.Wrappers;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class DamageBookHandler(IBookRepository bookRepository) : IRequestHandler<DamageBookCommand, ResponseResult>
{
	private readonly IBookRepository _bookRepository = bookRepository;

	public async Task<ResponseResult> Handle(DamageBookCommand request, CancellationToken cancellationToken)
    {
		var book = await _bookRepository.GetByIdAsync(request.Id);
		if (book == null)
			return new(false, $"Book of id: '{request.Id}' do not exist.");

		book.Damaged();
		await _bookRepository.UpdateAsync(book);
		return new(true, string.Empty);
	}
}
