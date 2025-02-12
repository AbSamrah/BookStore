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
    }
}
