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
    }
}
