using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
