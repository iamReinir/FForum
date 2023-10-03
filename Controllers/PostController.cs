using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
    
    public class PostController : Controller
    {
        private readonly PostSet _postSet = new();
        [Route("Post")]
        public IActionResult Index()
        {
            List<Post> objPost = _postSet.GetPosts().ToList();
            return View(objPost);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost()
        {
            
            return View();
        }
    }


}
