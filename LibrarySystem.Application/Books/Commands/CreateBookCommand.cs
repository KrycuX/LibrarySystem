using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Books.Commands;

public class CreateBookCommand : IRequest<Guid>
{
	public string Title { get; set; }
	public string Author { get; set; }
	public string ISBN { get; set; }
}
