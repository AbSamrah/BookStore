using Microsoft.AspNetCore.Mvc;

namespace BookStore.UI.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
