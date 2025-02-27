using BookStore.UI.Models;
using BookStore.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.UI.Controllers
{
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<BookDto> response = new List<BookDto>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7167/api/books/");
                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<BookDto>>());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {

        }
    }
}
