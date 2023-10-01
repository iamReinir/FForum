using forum.Database;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace forum.Controllers
{
    public class UserProfileController : Controller
    {
        [Route("profile")]
        public IActionResult UserProfile()
        {
            ISession session = HttpContext.Session;
            string? username = session.GetString("username");
            if (username == null)
                return View("NotFound");
            var uset = new UserSet();
            return View("UserProfile", uset.GetUser(username));
        }
    }
}
