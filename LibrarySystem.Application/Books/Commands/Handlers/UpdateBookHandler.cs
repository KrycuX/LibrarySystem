using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.Books.Commands;
using LibrarySystem.Shared.Wrappers;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class UpdateBookHandler(IBookRepository bookRepository) : IRequestHandler<UpdateBookCommand, ResponseResult>
{
	private readonly IBookRepository _bookRepository = bookRepository;

	public async Task<ResponseResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
		var book = await _bookRepository.GetByIdAsync(request.Id);
		if (book == null)
			return new(false, $"Book of id: '{request.Id}' do not exist.");

		book.ShelfLocation = request.ShelfLocation;
		book.ISBN = request.ISBN;
		book.Author = request.Author;
		book.Title = request.Title;

		await _bookRepository.UpdateAsync(book);
		return new(true, string.Empty);

	}
}
