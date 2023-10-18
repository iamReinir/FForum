using forum.Database;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using static forum.Controllers.UserUpdateController;
using System.IO;
using System.Text.Json;

namespace forum.Controllers
{    
    /*
    public class LikeController : Controller
    {
        [Route("/like")]
        [HttpPost]
        public IActionResult LikeAction()
        {
            StreamReader reader = new StreamReader(HttpContext.Request.Body);
            var x = reader.ReadLineAsync();
            UserSet uset = new UserSet();
            PostSet pset = new PostSet();
            string? username = HttpContext.Session.GetString("username");
            var user = uset.GetUser(username);                                 
            x.Wait();
            int post_id = int.Parse(x.Result);
            var like = (new LikeSet()).NewLike(post_id, user.ID);                                        
            return StatusCode(200);
        }

        [Route("like")]
        [HttpGet]
        public IActionResult Get() {
            return View("Error");
        }
    }
    */
}
