using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
{
    public Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
