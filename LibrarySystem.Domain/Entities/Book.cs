using LibrarySystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Domain.Entities;

public class Book
{
	public Guid Id { get; set; }
	[Required]
	public string ISBN { get; set; } = string.Empty;
	[Required]
	public string Title { get; set; } = string.Empty;
    [Required]
	public string Author { get; set; } = string.Empty;
    public BookStatus Status { get; set; }
	public string? ShelfLocation { get; set; }
	public string? BorrowedBy { get; set; }


	public void Borrow(string borrowedBy)
	{
		if (Status is not BookStatus.OnShelf) throw new InvalidOperationException("Book is not available for borrowing");

		Status = BookStatus.Borrowed;
		BorrowedBy = borrowedBy;
		ShelfLocation = null;
	}
	public void Return()
	{
		if (Status is not BookStatus.Borrowed) throw new InvalidOperationException("Book is not currently borrowed");

		Status = BookStatus.Returned;
		BorrowedBy = null;
	}
	public void Shelve(string location)
	{
		if (Status is not (BookStatus.Returned or BookStatus.Damaged)) throw new InvalidOperationException("Book cannot be shelved");

		Status = BookStatus.OnShelf;
		ShelfLocation = location;
	}
	public void Damaged()
	{
		if (Status is not (BookStatus.OnShelf or BookStatus.Returned)) throw new InvalidOperationException("Book cannot be set as damaged");

		Status = BookStatus.Damaged;
	}
}
