using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using X.PagedList;
using System.Web;
using X.PagedList.Mvc;

namespace forum.Controllers
{
	public class UserList
	{
		public ICollection<User> data = new List<User>();
	}
	public class AdminController : Controller
	{
		private IMongoCollection<User> _users;
		UserSet uset = new UserSet();
		public AdminController()
		{

		}
		[Route("/admin_page")]

		public ActionResult Index(int? page)
		{
			ISession session = HttpContext.Session;
			UserSet uset = new UserSet();
			string? username = session.GetString("username");
			var user = uset.GetUser(username);
			UserList list = new UserList();
			if (user == null)
				return View("Error");
			if (user.Role != Models.User.ROLE.ADMIN)
				return View("Error");
			Uri url = new Uri(HttpContext.Request.GetDisplayUrl());
			string? search_for = HttpUtility
				.ParseQueryString(url.Query)
				.Get("search_for");
			Console.WriteLine(search_for);
			if (search_for == null || search_for == "")
			{
				var pageNumber = page ?? 1;
				var pageSize = 5;
				var users = uset.GetUserList().ToPagedList(pageNumber, pageSize);
				return View(users);
			}
			else
			{
				list.data = uset.FindUsers(search_for);
				return View("Admin_page", list);
			}
			//public IActionResult Index()
			//{
			//    ISession session = HttpContext.Session;
			//    UserSet uset = new UserSet();
			//    string? username = session.GetString("username");
			//    var user = uset.GetUser(username);
			//    if (user == null)
			//        return View("Error");
			//    if (user.Role != Models.User.ROLE.ADMIN)
			//        return View("Error");
			//    Uri url = new Uri(HttpContext.Request.GetDisplayUrl());
			//    string? search_for = HttpUtility
			//        .ParseQueryString(url.Query)
			//        .Get("search_for");
			//    Console.WriteLine(search_for);
			//    UserList list = new UserList();
			//    if (search_for == null || search_for == ""){ var pageNumber = page ?? 1;
			//var pageSize = 5;
			//var users = uset.GetUserList().ToPagedList(pageNumber, pageSize); }
			//        list.data = uset.GetUserList();
			//    else list.data = uset.FindUsers(search_for);
			//    return View("Admin_page", list);
			//}
		}
	}
}

