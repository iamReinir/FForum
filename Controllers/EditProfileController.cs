using Microsoft.AspNetCore.Mvc;
using forum.Database;
using forum.Models;

namespace forum.Controllers
{
    public class EditProfileController : Controller
    {
        [Route("/editProfile")]
        [HttpGet]
        public IActionResult EditProfile()
        {
            ISession session = HttpContext.Session;
            string? username = session.GetString("username");
            if (username == null)
                return View("NotFound");
            var uset = new UserSet();
            return View("editprofile", uset.GetUser(username));
        }

        [Route("/editProfile")]
        [HttpPost]
        public IActionResult Edit()
        {
            /// Get data from the form in the payload
            string? username = HttpContext.Session.GetString("username");
            string? displayname = HttpContext.Request.Form["display_name"];
            string? birthdate = HttpContext.Request.Form["birthdate"];
            string? address = HttpContext.Request.Form["address"];
            string? email = HttpContext.Request.Form["email"];
            string? telephone = HttpContext.Request.Form["telephone"];

            /// Get and check user
            var uset = new UserSet();
            User? user = uset.GetUser(username);
            if (user == null)
                return StatusCode(404); // Cant found user

            /// Shouldnt be null. user already checked.
            UserInfo? userInfo = user.UserInfo;

            userInfo.Name = displayname ?? userInfo.Name;
            userInfo.Address = address ?? userInfo.Address;
            userInfo.Telephone = telephone ?? userInfo.Telephone;
            userInfo.Email = email ?? userInfo.Email;
            // Birthdate is not used yet.
            uset.UpdateUser(user);
            new PostSet().UpdateAllPostOfUser(user);
            return Redirect("/profile");

        }
    }
}
