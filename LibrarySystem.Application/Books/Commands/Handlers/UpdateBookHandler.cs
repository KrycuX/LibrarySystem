using FluentValidation.Results;
using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Shared;
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
			throw new ValidationException(
				[
					new(nameof(Book), $"Book of id: '{request.Id}' do not exist.")
				]);


		// Sprawdzamy, czy użytkownik zmienia ISBN na inny
		if (book.ISBN != request.ISBN)
		{
			// Sprawdzamy, czy ten nowy ISBN już istnieje w bazie
			var isIsbnTaken = await _bookRepository.IsbnExist(request.ISBN);
			if (isIsbnTaken)
			{
				throw new ValidationException(
				[
					new(nameof(request.ISBN), "ISBN must be unique")
				]);
			}
		}

		book!.ShelfLocation = request.ShelfLocation;
		book.ISBN = request.ISBN;
		book.Author = request.Author;
		book.Title = request.Title;

		await _bookRepository.UpdateAsync(book);
		return new(true, string.Empty);

	}
}
