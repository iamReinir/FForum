using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace forum.Controllers
{
	public class Register : Controller
	{              
        
        [Route("register")]
        [HttpGet]
        public IActionResult SignupPage()
        {
        
            var uinfor = new UserSet();
            return View("Register", false);
        }



        [Route("register")]
        [HttpPost]
        public IActionResult Signup()
        {
            UserSet uset = new();
            string? username = HttpContext.Request.Form["username"];
            string? password = HttpContext.Request.Form["password"];
            if(username == null || password == null)
            {
                return View("Error");
            }
            int id = uset.Register(username, password);
            if(id == -1) return View("Error");
            UserInfo? info = uset.GetUserInfo(uset.GetUser(id));
            if(info == null) {
                return View("Error");
            }
            string? displayName = HttpContext.Request.Form["display_name"];
            string? email = HttpContext.Request.Form["email"];
            string? telephone = HttpContext.Request.Form["telephone"];
            string? address = HttpContext.Request.Form["address"];            
            info.Name = displayName ?? username;
            info.Email = email;
            info.Telephone = telephone;
			info.Address = address;
            uset.UpdateUserInfo(info);           
			return View("register", true);
        }		
    }
}
