using BookStore.UI.Models;
using BookStore.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

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
                var httpResponseMessage = await client.GetAsync("https://localhost:7167/api/books");
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
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7167/api/books"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };
        

            var httpResponseMessage =  await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<BookDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Books");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<BookDto>($"https://localhost:7167/api/books/{id.ToString()}");

            if(response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (BookDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7167/api/books/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponeMessage = await client.SendAsync(httpResponseMessage);
            httpResponeMessage.EnsureSuccessStatusCode();

            var response = await httpResponeMessage.Content.ReadFromJsonAsync<BookDto>();

            if(response is not null)
            {
                return RedirectToAction("Index", "Books");
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Delete(BookDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();


                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7167/api/books/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Books");
            }
            catch(Exception ex)
            {

            }

            return View();
        } 
    }
}
