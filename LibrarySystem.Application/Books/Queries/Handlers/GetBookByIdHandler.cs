using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.DTOs;
using MapsterMapper;
using MediatR;

namespace LibrarySystem.Application.Books.Queries;

internal class GetBookByIdHandler(IBookRepository bookRepository, IMapper mapper) : IRequestHandler<GetBookByIdQuery, BookDto?>
{
	private readonly IBookRepository _bookRepository = bookRepository;
	private readonly IMapper _mapper = mapper;

	public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
	{
		var book = await _bookRepository.GetByIdAsync(request.Id);
		if(book == null) 
			return null;

		return _mapper.Map<BookDto>(book);
	}
}
