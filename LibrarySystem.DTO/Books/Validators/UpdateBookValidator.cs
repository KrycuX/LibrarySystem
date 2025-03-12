using FluentValidation;
using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.Books.Commands;

namespace LibrarySystem.Shared.Books.Validators;

public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
{
	private readonly IBookRepository _bookRepository;

	public UpdateBookValidator(IBookRepository bookRepository)
	{
		_bookRepository = bookRepository;
		RuleFor(x => x.Title)
			.NotEmpty().WithMessage("Title is required");

		RuleFor(x => x.Author)
			.NotEmpty().WithMessage("Author is required");

		RuleFor(x => x.ISBN)
			.NotEmpty().WithMessage("ISBN is required")
			.Length(17).WithMessage("ISBN must be 17 chars")
			.MustAsync(CheckUnique).WithMessage("ISBN must be unique");
	}

	private async Task<bool> CheckUnique(string isbn, CancellationToken token)
	{
		return await _bookRepository.CheckIsbnAsync(isbn);
	}
}
