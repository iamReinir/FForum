using forum.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static forum.Models.User;

namespace forum.Controllers
{
	public class LoginController : Controller
	{
		public enum LoginState
		{
			success,
			wrong_info,
			server_failed,
			locked,
			none
		}
		LoginState currentState = LoginState.none;
		[Route("login")]
		[HttpGet]
		public IActionResult Index()
		{
			currentState = LoginState.none;
			return View("Index",currentState);
		}


		// Deal with login logic
		[Route("login")]
		[HttpPost]
		public IActionResult Login() {
			UserSet uset = new();
			string? username = HttpContext.Request.Form["username"];
			string? password = HttpContext.Request.Form["password"];
			if (username == null || password == null)
				return View("NotFound");
			var user = uset.Login(username,password);
			if (user == null)
			{
				currentState = LoginState.wrong_info;
				return View("Index", currentState);
			}
			if (user.Is_banned)
			{
				return View("Banned");
			}
			ISession session = HttpContext.Session;
			session.Set("username", Encoding.UTF8.GetBytes(username));
			session.Set("role", Encoding.ASCII.GetBytes(user.Role.ToString()));
			return Redirect("home");
		}
	}
}
