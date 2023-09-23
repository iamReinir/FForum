using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
	public class LoginController : Controller
	{
		[Route("login")]
		[HttpGet]
		public IActionResult Index()
		{
			
			return View("Index");
		}


		// Deal with login logic
		[Route("login")]
		[HttpPost]
		public IActionResult Login() {
			
			return View("NotFound");
		}
	}
}
