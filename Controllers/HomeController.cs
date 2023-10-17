using forum.Database;
using forum.DataDecorator;
using forum.Models;
using Microsoft.AspNetCore.Mvc;


namespace forum.Controllers
{
    public class HomePageModel
    {
        public List<Post> post_list = new();
    }
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/home")]
        [HttpGet]
        public IActionResult Index()
        {
            ISession session = HttpContext.Session;
            var uset = new UserSet().GetUserList();
            var pset = new PostSet().FindPost("");
            string?username = session.GetString("username");
            var model = new HomePageModel();
            model.post_list = pset.ToList();
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
            if(user == null)
            {
                //Unathozied
                return StatusCode(401);
            }
            string? content = HttpContext.Request.Form["content"];
            var postSet = new forum.Database.PostSet();
            var post = postSet.NewPost(user);
            var pinfo = post.Info;
            pinfo.Content= content;
            postSet.UpdatePost(post);
            return Redirect("/home");
        }
    }
}
