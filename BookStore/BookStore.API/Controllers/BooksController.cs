using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.API.models.Domain;
using BookStore.API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private IBookRepository bookRepository;
        private readonly IMapper mapper;
        private readonly ILogger<BooksController> logger;

        public BooksController(IBookRepository bookRepository, IMapper mapper , ILogger<BooksController> logger)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpGet]
        //[Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllBooks()
        {
            logger.LogInformation("GetAllBooks Action Method was invoked");

            var books = await bookRepository.GetAllAsync();

            logger.LogInformation($" Finished GetAllBooks request with data: {JsonSerializer.Serialize(books)}");

            var booksDTO = mapper.Map<List<models.DTO.Book>>(books);
            return Ok(booksDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetBookAsync")]
        //[Authorize(Roles = "reader")]
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
        //[Authorize(Roles = "writer")]
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
        //[Authorize(Roles = "writer")]
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
        //[Authorize(Roles = "writer")]
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


    }
}
