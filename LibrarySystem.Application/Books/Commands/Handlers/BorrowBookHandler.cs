using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class BorrowBookHandler : IRequestHandler<BorrowBookCommand>
{
    public Task Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
