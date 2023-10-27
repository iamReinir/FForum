using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Authentication;
using System.Runtime.Serialization;

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
        public async Task<IActionResult> Create()
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
            IFormFile? avatar = HttpContext.Request.Form.Files["picture"];

            var postSet = new forum.Database.PostSet();
            var post = postSet.NewPost(user);
            var pinfo = post.Info;
            pinfo.Content = content;
            if (avatar != null && avatar.Length > 0)
            {
                // Resize the image to 10MB
                var resizedImage = await MongoDBConst.ResizeImageTo10MBAsync(avatar);

                // Convert the resized image to Base64
                pinfo.Base64String = Convert.ToBase64String(resizedImage);
            }
            postSet.UpdatePost(post);
            return Redirect("/home");
        }

        [HttpPost]
        [Route("/editpost")]
        public IActionResult EditPost()
        {
            ISession session = HttpContext.Session;
            UserSet uset = new UserSet();
            string? username = session.GetString("username");
            string? content = HttpContext.Request.Form["content"];
            var user = uset.GetUser(username);
            int postId = int.Parse(HttpContext.Request.Query["post"]);
            var postSet = new forum.Database.PostSet();
            Post? post = postSet.FindPost(postId);
            if (post == null)
                return StatusCode(404);
            PostInfo? postInfo = post.Info;

            postInfo.Content = content ?? postInfo.Content;
            postSet.UpdatePost(post);
            return Redirect("/home");
        }

        [HttpPost]
        [Route("/search")]
        public IActionResult search()
        {
            ISession session = HttpContext.Session;
            
            string? username = session.GetString("username");
            string? search = HttpContext.Request.Form["search_for"];
             HomePageModel list = new HomePageModel();
            var uset = new UserSet().GetUserList();
            var pset = new PostSet().FindPost(search, username);
            var model = new HomePageModel();
            model.post_list = pset;
           

            return View("Index", model);

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

        public class MinimalComment
        {
            public int? Id { get; set; }
            public int? Post_id { get; set; }
            public string? Username { get; set; }
            public string? Displayname { get; set; }
            public string? Content { get; set; }
            public string? Base64String { get; set; }
            public DateTime? Create_date { get; set; }
        }
        [Route("/comment")]
        [HttpGet]
        public IActionResult Comment()
        {
            
            int postId = int.Parse(HttpContext.Request.Query["id"]);
            var postlist = new CommentSet().GetComments(postId);
            var res = new List<MinimalComment>();
            foreach (var item in postlist)
            {
                var x = new MinimalComment();
                x.Id = item.Id;
                x.Post_id = item.PostId;
                x.Username = item.User.Username;
                x.Displayname = item.User.UserInfo.Name;
                x.Content = item.Content;
                x.Create_date = item.Create_date;
                x.Base64String = item.User.UserInfo.Base64String;
                res.Add(x);
            }            
            return Content(System.Text.Json.JsonSerializer.Serialize(res));
        }

        [Route("/comment")]
        [HttpPost]
        public IActionResult PostComment()
        {
            string? username = HttpContext.Session.GetString("username");
            if (username == null) return StatusCode(401); // Not authorized
            StreamReader reader = new StreamReader(HttpContext.Request.Body);
            var x = reader.ReadToEndAsync();            
            int postId = int.Parse(HttpContext.Request.Query["id"]);
            Console.WriteLine(username + " comment on posts #" + postId);
           
            var commentSet = new CommentSet();
            Comment comment = commentSet
                .NewComment(new UserSet().GetUser(username), postId);
            x.Wait();
            var content = x.Result;
            comment.Content = content;
            commentSet.UpdateComment(comment);
            return StatusCode(200);
        }


    }
}
