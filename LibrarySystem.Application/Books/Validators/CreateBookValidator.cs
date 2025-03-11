using FluentValidation;
using LibrarySystem.Application.Books.Commands;

namespace LibrarySystem.Application.Books.Validators;

public class CreateBookValidator : AbstractValidator<CreateBookCommand>
{
	public CreateBookValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty().WithMessage("Title is required");

		RuleFor(x => x.Author)
			.NotEmpty().WithMessage("Author is required");

		RuleFor(x => x.ISBN)
			.NotEmpty().WithMessage("ISBN is required")
			.Length(13).WithMessage("ISBN must be 13 chars")
			.MustAsync(CheckUnique).WithMessage("ISBN must be unique");
	}

	private async Task<bool> CheckUnique(string isbn, CancellationToken token)
	{
		return await Task.Run(() => { return true; } );
	}
}
