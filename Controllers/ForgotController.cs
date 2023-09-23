using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
	public class ForgotController : Controller
	{
		[Route("Forgot")]
		public IActionResult Forgot()
		{
			return View("Forgot");
		}
		[Route("ResetPass")]
		public IActionResult ResetPass()
		{
			return View("ResetPass");
		}
	}
}
