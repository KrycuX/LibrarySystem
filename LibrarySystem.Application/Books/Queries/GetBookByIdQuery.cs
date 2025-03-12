using LibrarySystem.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Books.Queries;

public class GetBookByIdQuery: IRequest<BookDto?>
{
	public Guid Id { get; set; }
}
