using Microsoft.AspNetCore.Mvc;
using forum.Database;
using MongoDB.Bson.IO;
using System.Text.Json;

namespace forum.Controllers
{


    public class UserUpdateController : Controller
    {
        internal class ShortUserInfo
        {
            public string Username { get; set; }
            public int Role { get; set; }
            public bool Is_banned { get; set; }
            public string toString()
            {
                return Username + " " + Role + " " + Is_banned;
            }
        }
        [Route("update")]
        [HttpPost]
        public IActionResult Update()
        {
            ISession session = HttpContext.Session;
            string? username = session.GetString("username");
            if (username == null)
                return StatusCode(405); // not allowed
            var uset = new UserSet();
            var user = uset.GetUser(username);
            if (user.Role != Models.User.ROLE.ADMIN)
                return StatusCode(405); // not allowed
            try
            {
                StreamReader reader = new StreamReader(HttpContext.Request.Body);
                var x = reader.ReadLineAsync();
                x.Wait();
                var obj = JsonSerializer.Deserialize<ShortUserInfo>(x.Result);
                Console.WriteLine(obj.toString());
                var changeUser = uset.GetUser(obj.Username);
                if (changeUser != null)
                {
                    changeUser.Is_banned = obj.Is_banned;
                    changeUser.Role = obj.Role;
                    uset.UpdateUser(changeUser);
                    new PostSet().UpdateAllPostOfUser(user);
                }
            }
            catch
            {
                return StatusCode(500);
            }
            return StatusCode(200); // ok
        }

        [Route("update")]
        [HttpGet]
        public IActionResult Error()
        {
            return View("error");
        }

        [Route("deletepost")]
        [HttpPost]
        public IActionResult DeletePost()
        {
            var uset = new UserSet();
            var pset = new PostSet();
            var user = uset.GetUser(HttpContext.Session.GetString("username"));
            try
            {
                StreamReader reader = new StreamReader(HttpContext.Request.Body);
                var x = reader.ReadLineAsync();
                x.Wait();
                var post_id = int.Parse(x.Result);
                var post = pset.FindPost(post_id);
                if (post == null || user == null || post.Poster.Username != user.Username)
                    return StatusCode(403); // Not allowed
                pset.HidePost(post);
            }
            catch
            {
                return StatusCode(500);
            }
            return StatusCode(200); // ok
        }

        [Route("hidepost")]
        [HttpPost]
        public IActionResult HidePost()
        {
            var uset = new UserSet();
            var pset = new PostSet();
            var user = uset.GetUser(HttpContext.Session.GetString("username"));
            try
            {
                StreamReader reader = new StreamReader(HttpContext.Request.Body);
                var x = reader.ReadLineAsync();
                x.Wait();
                var post_id = int.Parse(x.Result);
                var post = pset.FindPost(post_id);
                
                if (post == null || user == null)
                    return StatusCode(403); // Not allowed
                pset.HidePost(post);
            }
            catch
            {
                return StatusCode(500);
            }
            return StatusCode(200); // ok
        }
    }
}
