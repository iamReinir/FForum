using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/home")]
        public IActionResult Index()
        {
            return View("Index");  
        }
    }
}
