using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class ShelveBookHandler : IRequestHandler<ShelveBookCommand>
{
    public Task Handle(ShelveBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
