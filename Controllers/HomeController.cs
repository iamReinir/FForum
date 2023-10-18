using forum.Database;
using forum.DataDecorator;
using forum.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Authentication;

namespace forum.Controllers
{
    public class HomePageModel
    {
        public ICollection<(Post,bool)> post_list;
    }
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/home")]
        [HttpGet]
        public IActionResult Index()
        {
            ISession session = HttpContext.Session;
            string? username = session.GetString("username");
            var uset = new UserSet().GetUserList();
            var pset = new PostSet().FindPost("",username);           
            var model = new HomePageModel();
            model.post_list = pset;
            return View("Index", model);

        }
        [HttpPost]
        [Route("/post")]
        public IActionResult Create()
        {
            ISession session = HttpContext.Session;
            UserSet uset = new UserSet();
            string? username = session.GetString("username");
            var user = uset.GetUser(username);
            if (user == null)
            {
                //Unathozied
                return StatusCode(401);
            }
            string? content = HttpContext.Request.Form["content"];
            var postSet = new forum.Database.PostSet();
            var post = postSet.NewPost(user);
            var pinfo = post.Info;
            pinfo.Content = content;
            postSet.UpdatePost(post);
            return Redirect("/home");
        }

        [Route("/like")]
        [HttpPost]
        public IActionResult Like()
        {
            
            int post_id = int.Parse(HttpContext.Request.Query["post"]);            
            string? username = HttpContext.Session.GetString("username");
            if(username==null )
                return StatusCode(401); // Unauthorized 
            Console.WriteLine($"user {username} liked {post_id}");
            var likeSet = new LikeSet();
            likeSet.ToggleLike(username, post_id);
            return StatusCode(200);
        }
    }
}
