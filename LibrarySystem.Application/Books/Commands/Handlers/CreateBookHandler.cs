using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.DTOs;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class CreateBookHandler(IBookRepository bookRepository) : IRequestHandler<CreateBookCommand, BookDto>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
