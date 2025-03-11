using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class DamageBookHandler : IRequestHandler<DamageBookCommand>
{
    public Task Handle(DamageBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
