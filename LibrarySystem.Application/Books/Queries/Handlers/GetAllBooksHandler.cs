using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Wrappers;
using MediatR;

namespace LibrarySystem.Application.Books.Queries;

public class GetAllBooksHandler(IBookRepository bookRepository) : IRequestHandler<GetAllBooksQuery, ResponseResult<PaginatedResult<BookDto>>>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<ResponseResult<PaginatedResult<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var items = await _bookRepository.GetItemsAsync(request.Page, request.PageSize, request.SortBy, request.Descending);
        return new(true, items,string.Empty);
    }
}

