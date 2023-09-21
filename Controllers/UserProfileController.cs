using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
    public class UserProfileController : Controller
    {
        [Route("profile")]
        public IActionResult UserProfile()
        {
            ISession session = HttpContext.Session;
            byte[]? username_data = session.Get("username");
            if (username_data == null)
                return View("NotFound");
            return View("UserProfile");
        }
    }
}
