using FluentValidation.Results;

namespace LibrarySystem.Shared;

public class ValidationException : Exception
{
	public List<ValidationFailure> Errors { get; }

	public ValidationException(List<ValidationFailure> errors)
		: base("Validation failed")
	{
		Errors = errors;
	}
}
