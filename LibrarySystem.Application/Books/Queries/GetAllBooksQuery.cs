﻿using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Helpers;
using MediatR;

namespace LibrarySystem.Application.Books.Queries;

public class GetAllBooksQuery : IRequest<PaginatedResult<BookDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public SortByEnum? SortBy { get; set; }
    public bool Descending { get; set; }

}
