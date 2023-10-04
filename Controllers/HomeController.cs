using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Mvc;


namespace forum.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/home")]
        [HttpGet]
        public IActionResult Index()
        {
            ISession session = HttpContext.Session;
            UserSet uset = new UserSet();

            string?username = session.GetString("username");
            var user = uset.GetUser(username);
            return View("Index",user);  
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
            var pinfo = postSet.GetPostInfo(post);
            pinfo.Content= content;
            postSet.UpdatePostInfo(pinfo);

            
            return Redirect("/home");
        }
    }
}
