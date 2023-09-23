using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/index")]
        public IActionResult Index()
        {
            return View("Index");  
        }
    }
}
