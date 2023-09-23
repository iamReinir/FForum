using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
	public class Register : Controller
	{
		[Route("register")]
		public IActionResult Index()
		{
			return View("Register");
		}
	}
}
