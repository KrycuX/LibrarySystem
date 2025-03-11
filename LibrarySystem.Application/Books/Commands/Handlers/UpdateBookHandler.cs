using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class UpdateBookHandler : IRequestHandler<UpdateBookCommand>
{
    public Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
