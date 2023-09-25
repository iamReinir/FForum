using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
	public class LogoutController : Controller
	{
		
		[Route("logout")]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return Redirect("/login");
		}
	}
}
