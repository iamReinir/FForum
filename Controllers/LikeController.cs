using forum.Database;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using static forum.Controllers.UserUpdateController;
using System.IO;
using System.Text.Json;
using forum.Models;
using MongoDB.Driver;

namespace forum.Controllers
{    
    public class LikeController : Controller
    {

        [Route("/like")]
        [HttpPost]
        public IActionResult Like()
        {

            int post_id = int.Parse(HttpContext.Request.Query["post"]);
            string? username = HttpContext.Session.GetString("username");
            if (username == null)
                return StatusCode(401); // Unauthorized 
            Console.WriteLine($"user {username} liked {post_id}");
            var likeSet = new LikeSet();
            likeSet.ToggleLike(username, post_id);
            return StatusCode(200);
        }

        [Route("like")]
        [HttpGet]
        public IActionResult Get() {
            string? username = HttpContext.Session.GetString("username");
            if (username == null) return StatusCode(401); //Not authorized
            int postID = int.Parse(HttpContext.Request.Query["id"]);
            string result = "0";
            var hasLiked = MongoDBConst.database
                .GetCollection<Like>(MongoDBConst.LIKE_TABLE)
                .Find(like => like.Post_id == postID && like.Username == username)
                .ToList().FirstOrDefault() != null;
            if (hasLiked)
                result = "1";
            return Content(result);
        }
    }
}
