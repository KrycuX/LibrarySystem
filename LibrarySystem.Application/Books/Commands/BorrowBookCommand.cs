using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class BorrowBookCommand: IRequest
{
    public Guid Id { get; init; }
    public string BorrowedBy { get; init; } = string.Empty;
}
