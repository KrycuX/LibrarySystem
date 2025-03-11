using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Helpers;
using MediatR;

namespace LibrarySystem.Application.Books.Queries;

public class GetAllBooksHandler(IBookRepository bookRepository) : IRequestHandler<GetAllBooksQuery, PaginatedResult<BookDto>>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<PaginatedResult<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetItemsAsync(request.Page, request.PageSize, request.SortBy,request.Descending);
    }
}

