using FluentValidation;
using FluentValidation.Results;
using LibrarySystem.Shared.Books.Commands;
using LibrarySystem.Shared.Books.Query;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Helpers;
using LibrarySystem.Shared.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    public class BookController(IMediator mediator, ILogger<BookController> logger) : ControllerBase
    {

        private readonly ILogger<BookController> _logger = logger;
        private readonly IMediator _mediator = mediator;
        [HttpGet("GetBooks")]
        public async Task<ActionResult<PaginatedResult<BookDto>>> GetPaginatedBooks(
            [FromQuery]int page = 1,
            [FromQuery]int pageSize = 10,
            [FromQuery]SortByEnum? sortBy = null,
            [FromQuery]bool descending = false)
        {
            try
            {
                if (page < 0)
                    return BadRequest();

                var query = new GetAllBooksQuery
                {
                    Page = page,
                    PageSize = pageSize,
                    SortBy = sortBy,
                    Descending = descending
                };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex,ex.Message);
                return BadRequest(ex.Message);
            } 
        }

        [HttpPost("CreateBook")]
        public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookCommand command, [FromServices] IValidator<CreateBookCommand> validator)
        {
            try
            {
				var valid = await validator.ValidateAsync(command);
				if (!valid.IsValid)
                {
                    return BadRequest(valid);
                }

               var result = await _mediator.Send(command);
               return CreatedAtAction(nameof(CreateBook),result);
            }
			catch (LibrarySystem.Shared.ValidationException ex)
			{
                return BadRequest(new ValidationResult
                {
                    
                    Errors = ex.Errors.Select(e => new ValidationFailure
                    {
                        PropertyName = e.PropertyName,
                        ErrorMessage = e.ErrorMessage
                    }).ToList()
                });
			}
			catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("UpdateBook")]
        public async Task<ActionResult> UpdateBook([FromBody] UpdateBookCommand updateBookCommand, [FromServices] IValidator<UpdateBookCommand> validator)
        {
            try
            {
                var valid = await validator.ValidateAsync(updateBookCommand);
                if (!valid.IsValid)
                {
					return BadRequest(valid);
				}

                await _mediator.Send(updateBookCommand);
                return NoContent();
            }
			catch (Shared.ValidationException ex)
			{
				return BadRequest(new ValidationResult
				{

					Errors = ex.Errors.Select(e => new ValidationFailure
					{
						PropertyName = e.PropertyName,
						ErrorMessage = e.ErrorMessage
					}).ToList()
				});
			}
			catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteBook")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            try
            {
                var command = new DeleteBookCommand
                {
                    Id = id,
                
                };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("BorrowBook")]
        public async Task<ActionResult> BorrowBook(Guid id, [FromBody] string borrowedBy)
        {
            try
            {
                var command = new BorrowBookCommand
                {
                    Id = id,
                    BorrowedBy = borrowedBy
                };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("ReturnBook")]
        public async Task<ActionResult> ReturnBook(Guid id)
        {
            try
            {
                var command = new ReturnBookCommand
                {
                    Id = id
                };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("ShelveBook")]
        public async Task<ActionResult> ShelveBook(Guid id, [FromBody] string shelfLocation)
        {
            try
            {
                var command = new ShelveBookCommand
                {
                    Id = id,
                    ShelfLocation=shelfLocation
                };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("SetDamageBook")]
        public async Task<ActionResult> SetDamageBook(Guid id)
        {
            try
            {
                var command = new DamageBookCommand
                {
                    Id = id,
                };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
