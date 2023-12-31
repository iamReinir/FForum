﻿using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace forum.Controllers
{
	public class RegisterController : Controller
	{              
        

        public enum RegisterState
        {
            success,
            password_too_short,
            duplicated_username,
            error,
            none
        } 
        [Route("register")]
        [HttpGet]
        public IActionResult SignupPage()
        { 
            return View("Register", RegisterState.none);
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Signup()
        {
            UserSet uset = new();
            string? username = HttpContext.Request.Form["username"];
            string? password = HttpContext.Request.Form["password"];
            if(username == null || password == null || username == "" || password == "")
            {
                return View("Error");
            }
            if(password.Length < 6) { return View("register", RegisterState.password_too_short); }
            var user = uset.Register(username, password);
            if(user == null) return View("register", RegisterState.duplicated_username);                       
            UserInfo? info = user.UserInfo;
            string? displayName = HttpContext.Request.Form["display_name"];
            string? email = HttpContext.Request.Form["email"];
            string? telephone = HttpContext.Request.Form["telephone"];
            string? address = HttpContext.Request.Form["address"];            
            info.Name = displayName ?? username;
            info.Email = email;
            info.Telephone = telephone;
			info.Address = address;
            uset.UpdateUser(user);           
			return View("register", RegisterState.success);
        }		
    }
}
