using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class ReturnBookHandler : IRequestHandler<ReturnBookCommand>
{
    public Task Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
