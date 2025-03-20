using FluentValidation;
using LibrarySystem.Shared.Books.Commands;

namespace LibrarySystem.Shared.Books.Validators;

public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
{


	public UpdateBookValidator()
	{

		RuleFor(x => x.Title)
			.NotEmpty().WithMessage("Title is required");

		RuleFor(x => x.Author)
			.NotEmpty().WithMessage("Author is required");

		RuleFor(x => x.ISBN)
			.NotEmpty().WithMessage("ISBN is required")
			.Length(17).WithMessage("ISBN must be 17 chars");
	}

}
