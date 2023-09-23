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
            session.Set("username", Encoding.UTF8.GetBytes("reinir"));
            byte[]? username_data = session.Get("username");
            if (username_data == null)
                return View("NotFound");
            return View("UserProfile");
        }
    }
}
