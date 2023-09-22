using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
	public class Register : Controller
	{
		[Route("Register")]
		public IActionResult Index()
		{
			return View("Register");
		}
	}
}
