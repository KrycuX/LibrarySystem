using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.Context;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Helpers;
using LibrarySystem.Shared.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Repositories;

internal sealed class BookRepository(LibraryDbContext libraryDbContext) : IBookRepository
{
    private readonly LibraryDbContext _libraryDbContext = libraryDbContext;

    public async Task<PaginatedResult<BookDto>> GetItemsAsync(int page, int pageSize, SortByEnum? sortBy, bool descending)
    {
        var query = _libraryDbContext.Books.AsQueryable();

        query = sortBy switch
        {
            SortByEnum.Title => descending ? query.OrderByDescending(i => i.Title) : query.OrderBy(i => i.Title),
            SortByEnum.Author => descending ? query.OrderByDescending(i => i.Author) : query.OrderBy(i => i.Author),
            SortByEnum.Status => descending ? query.OrderByDescending(i => i.Status) : query.OrderBy(i => i.Status),
            SortByEnum.ISBN => descending ? query.OrderByDescending(i => i.ISBN) : query.OrderBy(i => i.ISBN),
            _ => query.OrderBy(i => i.Id)
        };

        var totalItems = await query.CountAsync();

        var items = await query.Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .Select(i => new BookDto
                                {
                                    Id = i.Id,
                                    ISBN = i.ISBN, 
                                    Title = i.Title,
                                    Author = i.Author,
                                    BorrowedBy = i.BorrowedBy,
                                    ShelfLocation = i.ShelfLocation,
                                    Status = (BookStatusDto)i.Status,

                                })
                                .ToListAsync();
        return new PaginatedResult<BookDto>(items, totalItems,page,pageSize, sortBy,descending);
    }
    public async Task<Book> AddAsync(Book book)
    {
        await _libraryDbContext.Books.AddAsync(book);
        
        await _libraryDbContext.SaveChangesAsync();
        var bookToReturn = await _libraryDbContext.Books.FindAsync(book.Id);
        if (bookToReturn is null)
            throw new Exception("Book is not added properly");
        return bookToReturn;
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await _libraryDbContext.Books.FindAsync(id);
        if (book != null)
        {
            _libraryDbContext.Books.Remove(book);
            await _libraryDbContext.SaveChangesAsync();
        }
    }
    public async Task UpdateAsync(Book book)
    {
        _libraryDbContext.Books.Update(book);
        await _libraryDbContext.SaveChangesAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _libraryDbContext.Books.FindAsync(id);
    }

	public async Task<bool> CheckIsbnAsync(string isbn)
	{
		return !await _libraryDbContext.Books.AnyAsync(x=>x.ISBN.Equals(isbn));
	}
}
