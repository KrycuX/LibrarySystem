﻿
using LibrarySystem.Domain.Entities;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Helpers;
using LibrarySystem.Shared.Wrappers;

namespace LibrarySystem.Application.Common.Interfaces;

public interface IBookRepository
{
    Task<PaginatedResult<BookDto>> GetItemsAsync(int page, int pageSize, SortByEnum? sortBy, bool descending);
    Task<Book?> GetByIdAsync(Guid id);
    Task<Book> AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Guid id);
	Task<bool> IsbnExist(string isbn);
}
