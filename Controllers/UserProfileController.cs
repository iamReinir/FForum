using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
    public class UserProfileController : Controller
    {
        [Route("profile")]
        public IActionResult UserProfile()
        {
            return View("UserProfile");
        }
    }
}
