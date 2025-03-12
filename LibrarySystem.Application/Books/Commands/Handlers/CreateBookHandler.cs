using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Shared.Books.Commands;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Wrappers;
using MapsterMapper;
using MediatR;

namespace LibrarySystem.Application.Books.Commands;

public class CreateBookHandler(IBookRepository bookRepository, IMapper mapper) : IRequestHandler<CreateBookCommand, ResponseResult<BookDto?>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseResult<BookDto?>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        if (await _bookRepository.CheckIsbnAsync(request.ISBN))
            return new(false, null, "ISBN must be unique");
       var result = await _bookRepository.AddAsync(new()
        {
            Id= Guid.NewGuid(),
            Title = request.Title,
            Author= request.Author,
            ISBN = request.ISBN,
            ShelfLocation= request.ShelfLocation,
            Status = Domain.Enums.BookStatus.OnShelf
        });
	    var book=_mapper.Map<BookDto>(result);
		return new(true,book,string.Empty);
    }
}
