using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using LibrarySystem.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Infrastructure.EntityFramework.Helpers;

public static class DbInit
{
    public static void Seed(IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

        dbContext.Database.EnsureCreated();

        if (!dbContext.Books.Any())
        {
            var books = new List<Book>
            {
                 new () { Id = Guid.NewGuid(), ISBN = "978-3-16-148410-0", Title = "C# in Depth", Author = "Jon Skeet", Status = BookStatus.OnShelf, ShelfLocation = "A1" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-13-110362-7", Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Status = BookStatus.Borrowed, BorrowedBy = "John Doe" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-1-59327-584-6", Title = "Eloquent JavaScript", Author = "Marijn Haverbeke", Status = BookStatus.Returned },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-262-03384-8", Title = "Introduction to Algorithms", Author = "Thomas H. Cormen", Status = BookStatus.Damaged, ShelfLocation = "B3" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-201-63361-0", Title = "Design Patterns", Author = "Erich Gamma", Status = BookStatus.OnShelf, ShelfLocation = "C2" },

                 new () { Id = Guid.NewGuid(), ISBN = "978-1-491-94732-4", Title = "Clean Code", Author = "Robert C. Martin", Status = BookStatus.Borrowed, BorrowedBy = "Jane Smith" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-596-52068-7", Title = "Refactoring", Author = "Martin Fowler", Status = BookStatus.OnShelf, ShelfLocation = "D4" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-1-491-93744-7", Title = "You Don't Know JS", Author = "Kyle Simpson", Status = BookStatus.Returned },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-262-03384-9", Title = "Computer Networking", Author = "James Kurose", Status = BookStatus.OnShelf, ShelfLocation = "E1" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-1-59327-584-7", Title = "The Art of Computer Programming", Author = "Donald Knuth", Status = BookStatus.Damaged, ShelfLocation = "F5" },

                 new () { Id = Guid.NewGuid(), ISBN = "978-0-07-352332-3", Title = "Operating System Concepts", Author = "Abraham Silberschatz", Status = BookStatus.OnShelf, ShelfLocation = "G2" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-134-35492-7", Title = "Modern Operating Systems", Author = "Andrew S. Tanenbaum", Status = BookStatus.Borrowed, BorrowedBy = "Alice Johnson" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-134-35768-3", Title = "Artificial Intelligence", Author = "Stuart Russell", Status = BookStatus.Returned },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-201-16281-6", Title = "Compilers: Principles, Techniques, and Tools", Author = "Alfred V. Aho", Status = BookStatus.OnShelf, ShelfLocation = "H3" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-1-4493-8153-8", Title = "The Clean Coder", Author = "Robert C. Martin", Status = BookStatus.Damaged, ShelfLocation = "I4" },

                 new () { Id = Guid.NewGuid(), ISBN = "978-0-262-13472-6", Title = "Reinforcement Learning", Author = "Richard S. Sutton", Status = BookStatus.OnShelf, ShelfLocation = "J1" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-262-16209-5", Title = "Deep Learning", Author = "Ian Goodfellow", Status = BookStatus.Borrowed, BorrowedBy = "Bob Williams" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-1-4493-6954-2", Title = "JavaScript: The Good Parts", Author = "Douglas Crockford", Status = BookStatus.Returned },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-321-49621-3", Title = "Code Complete", Author = "Steve McConnell", Status = BookStatus.OnShelf, ShelfLocation = "K3" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-596-51774-9", Title = "Programming Pearls", Author = "Jon Bentley", Status = BookStatus.Damaged, ShelfLocation = "L2" },

                 new () { Id = Guid.NewGuid(), ISBN = "978-1-4919-1211-0", Title = "The Mythical Man-Month", Author = "Frederick P. Brooks Jr.", Status = BookStatus.OnShelf, ShelfLocation = "M1" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-1-59327-757-4", Title = "Hackers & Painters", Author = "Paul Graham", Status = BookStatus.Borrowed, BorrowedBy = "Chris Brown" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-321-91454-7", Title = "Effective Java", Author = "Joshua Bloch", Status = BookStatus.Returned },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-07-161586-0", Title = "Introduction to the Theory of Computation", Author = "Michael Sipser", Status = BookStatus.OnShelf, ShelfLocation = "N4" },
                 new () { Id = Guid.NewGuid(), ISBN = "978-0-262-03384-0", Title = "Artificial Intelligence: A Modern Approach", Author = "Stuart Russell", Status = BookStatus.Damaged, ShelfLocation = "O5" }
            };

            dbContext.Books.AddRange(books);
            dbContext.SaveChanges();
        }
    }
}
