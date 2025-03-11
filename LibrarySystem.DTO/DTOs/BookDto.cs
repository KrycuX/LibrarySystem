using LibrarySystem.Shared.Helpers;

namespace LibrarySystem.Shared.DTOs;

public class BookDto
{
    public Guid Id { get; init; }
    public string ISBN { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public BookStatusDto Status { get; init; }
    public string? ShelfLocation { get; init; }
    public string? BorrowedBy { get; init; }
}
