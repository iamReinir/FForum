using forum.Database;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace forum.Controllers
{
	public class Register : Controller
	{
        //public enum LoginState
        //{
        //    success,
        //    wrong_info,
        //    server_failed,
        //    locked,
        //    none
        //}
        //LoginState currentState = LoginState.none;
        [Route("register")]
        [HttpGet]
        public IActionResult Index()
        {
            return View("");
        }


        
        [Route("register")]
        [HttpPost]
        public IActionResult Signup()
        {
            UserSet uset = new();
            string? username = HttpContext.Request.Form["username"];
            string? password = HttpContext.Request.Form["password"];
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
			return View("Index",currentState);
		}


		// Deal with login logic
		[Route("login")]
		[HttpPost]
		public IActionResult Login() {
			UserSet uset = new();
			string? username = HttpContext.Request.Form["username"];
			string? email = HttpContext.Request.Form["email"];
            string? number = HttpContext.Request.Form["number"];
            string? pass = HttpContext.Request.Form["pwd"];
            string? rpass = HttpContext.Request.Form["pwd-repeat"];
            if (!(rpass).Equals(pass))
                return Redirect("login");
            var user = uset.GetUser(username);
			if (user == null || user.Password != password)
			{
				currentState = LoginState.wrong_info;
				return View("Index", currentState);
			}
			ISession session = HttpContext.Session;
			session.Set("username", Encoding.UTF8.GetBytes(username));
			return Redirect("home");
		}
            if (username == null || password == null)
                return View("NotFound");
            var user = uset.GetUser(username);
            if (user == null || user.Password != password)
            {
                currentState = LoginState.wrong_info;
                return View("Index", currentState);
            }
            ISession session = HttpContext.Session;
            session.Set("username", Encoding.UTF8.GetBytes(username));
            return Redirect("home");
        }
    }
}
