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
            var book = new models.Domain.Book()
            {
                Name = addBookRequest.Name,
                AuthorName = addBookRequest.AuthorName,
                CreatedDate = addBookRequest.CreatedDate,
                PriceInSYR = addBookRequest.PriceInSYR
            };

            book = await bookRepository.AddBookAsync(book);

            var bookDTO = new models.DTO.Book()
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
        public async Task<IActionResult> UpdateAsync(Guid id, models.DTO.UpdateBookRequest updateBookRequest)
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
    }
}
