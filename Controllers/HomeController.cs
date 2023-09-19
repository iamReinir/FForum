using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();  
        }
    }
}
