using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
    public class AdminController:Controller
    {
        [Route("/admin_page")]
        public IActionResult Index()
        {
            ISession session = HttpContext.Session;
            UserSet uset = new UserSet();

            string? username = session.GetString("username");
            var user = uset.GetUser(username);
            if(user == null)            
                return View("Error");            
            if (user.Role != Models.User.ROLE.ADMIN)
                return View("Error");
            return View("Admin_page", user);
        }
    }
}

