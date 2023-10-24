using Microsoft.AspNetCore.Mvc;
using forum.Database;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using X.PagedList.Mvc.Core;
using X.PagedList;

namespace forum.Controllers
{
    public class PostController : Controller
    {
        [Route("Paging")]
        public IActionResult ShowUserPaging(int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pagesize = 6;
            PostSet pset = new PostSet();
            var posts = pset.GetPosts().ToPagedList(page, pagesize);
            return PartialView(posts);
        }
    }
}
