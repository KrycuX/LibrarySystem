using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Context;

internal sealed class LibraryDbContext: DbContext
{
	public DbSet<Book> Books { get; set; }

}
