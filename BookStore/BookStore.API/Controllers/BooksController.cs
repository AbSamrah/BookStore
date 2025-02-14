using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.API.models.Domain;
using BookStore.API.Repositories;
using AutoMapper;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookRepository bookRepository;
        private readonly IMapper mapper;

        public BooksController(IBookRepository bookRepository,IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await bookRepository.GetAllAsync();

            var booksDTO = mapper.Map<List<models.DTO.Book>>(books);
            return Ok(booksDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetBookAsync")]
        public async Task<IActionResult> GetBookAsync(Guid id)
        {
            var book = await bookRepository.GetByIdAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            var bookDTO = mapper.Map<models.DTO.Book>(book);
            return Ok(bookDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddBookAsync(models.DTO.AddBookRequest addBookRequest)
        {
            if (!ValidateAddBookAsync(addBookRequest))
            {
                return BadRequest(ModelState);
            }

            var book = new models.Domain.Book()
            {
                Name = addBookRequest.Name,
                AuthorName = addBookRequest.AuthorName,
                CreatedDate = addBookRequest.CreatedDate,
                PriceInSYR = addBookRequest.PriceInSYR
            };

            book = await bookRepository.AddBookAsync(book);

            var bookDTO = new models.DTO.Book
            {
                Id = book.Id,
                Name = book.Name,
                CreatedDate = book.CreatedDate,
                AuthorName = book.AuthorName,
                PriceInSYR = book.PriceInSYR
            };

            return CreatedAtAction(nameof(GetBookAsync), new { id = bookDTO.Id }, bookDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            var book = await bookRepository.DeleteAsync(id);

            if(book == null)
            {
                return NotFound();
            }

            var bookDTO = mapper.Map<models.DTO.Book>(book);
            return Ok(bookDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] Guid id,[FromBody] models.DTO.UpdateBookRequest updateBookRequest)
        {
            var book = new models.Domain.Book()
            {
                Name = updateBookRequest.Name,
                CreatedDate = updateBookRequest.CreatedDate,
                AuthorName = updateBookRequest.AuthorName,
                PriceInSYR = updateBookRequest.PriceInSYR
            };

            book = await bookRepository.UpdateAsync(id, book);
            if(book == null)
            {
                return NotFound();
            }

            var bookDTO = mapper.Map<models.DTO.Book>(book);
            return Ok(bookDTO);
        }


        #region Private methods
        private bool ValidateAddBookAsync(models.DTO.AddBookRequest addBookRequest)
        {
            if (addBookRequest == null)
            {
                ModelState.AddModelError(nameof(addBookRequest), $"{nameof(addBookRequest)} cannot be null.");
                return false;
            }

            if (String.IsNullOrWhiteSpace(addBookRequest.Name)){
                ModelState.AddModelError(nameof(addBookRequest.Name), $"{nameof(addBookRequest.Name)} cannot be null or empty or white space.");
            }

            if (String.IsNullOrWhiteSpace(addBookRequest.AuthorName)){
                ModelState.AddModelError(nameof(addBookRequest.AuthorName), $"{nameof(addBookRequest.AuthorName)} cannot be null or empty or white space.");
            }

            if(addBookRequest.PriceInSYR < 0)
            {
                ModelState.AddModelError(nameof(addBookRequest.PriceInSYR), $"{nameof(addBookRequest.PriceInSYR)} cannot be negative.");
            }

            if(addBookRequest.CreatedDate > DateTime.Now)
            {
                ModelState.AddModelError(nameof(addBookRequest.CreatedDate), $"{nameof(addBookRequest.CreatedDate)} cannot be after now.");
            }

            if(ModelState.Count > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateBookAsync(models.DTO.UpdateBookRequest updateBookRequest)
        {
            if (updateBookRequest == null)
            {
                ModelState.AddModelError(nameof(updateBookRequest), $"Cannot be null.");
                return false;
            }

            if (String.IsNullOrWhiteSpace(updateBookRequest.Name))
            {
                ModelState.AddModelError(nameof(updateBookRequest.Name), $"{nameof(updateBookRequest.Name)} cannot be null or empty or white space.");
            }

            if (String.IsNullOrWhiteSpace(updateBookRequest.AuthorName))
            {
                ModelState.AddModelError(nameof(updateBookRequest.AuthorName), $"{nameof(updateBookRequest.AuthorName)} cannot be null or empty or white space.");
            }

            if (updateBookRequest.PriceInSYR < 0)
            {
                ModelState.AddModelError(nameof(updateBookRequest.PriceInSYR), $"{nameof(updateBookRequest.PriceInSYR)} cannot be negative.");
            }

            if (updateBookRequest.CreatedDate > DateTime.Now)
            {
                ModelState.AddModelError(nameof(updateBookRequest.CreatedDate), $"{nameof(updateBookRequest.CreatedDate)} cannot be after now.");
            }

            if (ModelState.Count > 0)
            {
                return false;
            }

            return true;
        }

        
        #endregion
    }
}
