using LibrarySystem.Domain.Enums;
using LibrarySystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities;

public class Book
{
	public Guid Id { get; private set; }
	public string ISBN { get; private set; }
	public string Title { get; private set; }
	public Author Author { get; private set; }
	public BookStatus Status { get; private set; }
	public string? ShelfLocation { get;private set; }
	public string? BorrowedBy { get; private set; }


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
		if (Status is not BookStatus.Returned || Status is not BookStatus.Damaged) throw new InvalidOperationException("Book cannot be shelved");

		Status = BookStatus.OnShelf;
		ShelfLocation = location;
	}
}
