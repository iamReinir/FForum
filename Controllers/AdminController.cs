using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace forum.Controllers
{
    public class UserList
    {
        public ICollection<User> data = new List<User>();
    }
    public class AdminController:Controller
    {
        [Route("/admin_page")]
        public IActionResult Index()
        {
            ISession session = HttpContext.Session;
            UserSet uset = new UserSet();
            Uri url = new Uri(HttpContext.Request.GetDisplayUrl());
            string? search_for = HttpUtility
                .ParseQueryString(url.Query)
                .Get("search_for");
            Console.WriteLine(search_for);
            string? username = session.GetString("username");
            var user = uset.GetUser(username);
            UserList list = new UserList();
            if (search_for == null || search_for == "")
                list.data = uset.GetUserList();
            else list.data = uset.FindUsers(search_for);
            if(user == null)            
                return View("Error");            
            if (user.Role != Models.User.ROLE.ADMIN)
                return View("Error");
            return View("Admin_page", list);
        }
    }
}

