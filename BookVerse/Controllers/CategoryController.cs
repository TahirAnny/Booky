using Microsoft.AspNetCore.Mvc;

namespace BookVerse.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
