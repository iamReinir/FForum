﻿using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
	public class LoginController : Controller
	{
		[Route("Login")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
