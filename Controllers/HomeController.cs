using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Mvc;


namespace forum.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/home")]
        public IActionResult Index()
        {
            ISession session = HttpContext.Session;
            UserSet uset = new UserSet();

            string?username = session.GetString("username");
            var user = uset.GetUser(username);
            return View("Index",user);  
        }

        [HttpPost]
        [Route("/home")]
        public IActionResult Create()
        {
            return View();
        }
    }
}
