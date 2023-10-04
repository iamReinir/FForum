using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using forum.Models;
using forum.Database;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace forum.Controllers
{
	public class ForgotController : Controller
	{
		public enum LoginState
		{
			success,
			notmatch,
			Otp_notmatch,
			none
		}
		LoginState currentState = LoginState.none;
		UserSet _userSet = new UserSet();
		private bool IsValidEmail(String Email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(Email);
				return addr.Address == Email;
			}
			catch { return false; }
		}
		[Route("Forgot")]
		public IActionResult Forgot()
		{
			return View("Forgot");
		}
		[Route("Forgot")]
		[HttpPost]
		public IActionResult ForgotPass()
		{
			//Verify UserName
			//Generate OTP
			//Send Email 
			string message = "";
			string? username = HttpContext.Request.Form["name"];
			var user = _userSet.GetUser(username);

			if (user == null)
			{
				currentState = LoginState.none;
				return View("Forgot", currentState);
			}
	
				var account = _userSet.GetUserInfo(user);
			if (IsValidEmail(account.Email) == false)
			{

				currentState = LoginState.notmatch;
				return View("Forgot", currentState);
			}
			//Send email for reset password
			SendOtpToEmail(account.Email, "ResetPassword");
			HttpContext.Session.SetString("username", username);
			return Redirect("/EnterOtp");
		}
		//otp
		[Route("/EnterOtp")]
		public IActionResult EnterOtp()
		{
			return View("EnterOtp");
		}
		//moi duoc them vao
		[Route("/EnterOtp")]
		[HttpPost]
		public IActionResult EnterOTP()
		{
			//		string message = "";
			string? valueOtp = HttpContext.Request.Form["value"];
			int value = int.Parse(valueOtp);
			int? otpp = HttpContext.Session.GetInt32("otp");
			if (value != otpp)
			{
				currentState = LoginState.Otp_notmatch;
				return View("EnterOtp", currentState);

			}

			//		message = "success";//
			return Redirect("ResetPass");
		}


		//reset
		[Route("/ResetPass")]
		public IActionResult ResetPass()
		{
			return View("ResetPass");
		}

		[Route("/ResetPass")]
		[HttpPost]
		public IActionResult Reset()
		{

			string? name = HttpContext.Session.GetString("username");
			string? pass = HttpContext.Request.Form["password"];
			string? cpass = HttpContext.Request.Form["confPass"];
			User? users = _userSet.GetUser(name);
			if (
				users == null || pass == null || pass != cpass)
			{

				currentState = LoginState.notmatch;
				return View("ResetPass", currentState);

			}
			var edit = _userSet.ResetPassword(name, pass);
			if (edit == true)
			{
				return Redirect("login");
			}

			return View("ResetPass");
		}


		//Send to Email
		[NonAction]
		public void SendOtpToEmail(string email, string emailFor)
		{

			var fromEmail = new MailAddress("sinhhoctebao0903@gmail.com", "ADMIN from FFORUM");
			var toEmail = new MailAddress(email);
			
			var fromEmailPassword = "wrzo fbte kkbq cyhx"; // Replace with actual password
			string subject = "";
			string body = "";
			int otpvalue = 0;
			
			if (emailFor == "ResetPassword")
			{
				Random rand = new Random();
				otpvalue = rand.Next(100000, 999999);
				subject = "Reset Password";
				HttpContext.Session.SetInt32("otp", otpvalue);
				body = "Hi,<br/>We got request for reset your account password. Please enter otp to reset your password" +
					"<br/><br/><p>" + otpvalue + "</p>";
			}
			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
			};

			using (var message = new MailMessage(fromEmail, toEmail)
			{
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			})
				smtp.Send(message);



		}
	}
}


