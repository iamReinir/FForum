using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using forum.Models;
using forum.Database;
using System.Runtime.CompilerServices;

namespace forum.Controllers
{
	public class ForgotController: Controller
	{
		private readonly IHttpContextAccessor contxt;
		public ForgotController(IHttpContextAccessor httpContextAccessor)
		{
			contxt = httpContextAccessor;
		}

		UserSet _userSet;

		[Route("Forgot")]
		public IActionResult Forgot()
		{
			return View("Forgot");
		}

		[HttpPost]
		public IActionResult Forgot(String UName)
		//public ActionResult Forgot(String UName)
		{
			//Verify UserName
			//Generate OTP
			//Send Email 
			string message = "";
			bool status = false;
			var user = _userSet.GetUser(UName);
			var account = _userSet.GetUserInfo(user);
			if (account != null)
			{
				//Send email for reset password
				SendOtpToEmail(account.Email, "ResetPassword");

				message = "OTP has been sent to your email id.";
			}
			else
			{
				message = "Account not found";
			}

			ViewBag.Message = message;
			return View();
		}
		//otp
		[Route("EnterOtp")]
		public IActionResult EnterOtp()
		{
			return View("EnterOtp");
		}
		//moi duoc them vao
		public IActionResult EnterOtp(int value)
		{
			string message = "";
			int otp = (int)contxt.HttpContext.Session.GetInt32("otp");
			if (value == otp)
			{
				message = "Success";
				return View("ResetPass");
			}
			else
			{
				message = "Failed";
			}
			ViewBag.Message = message;
			return View("EnterOtp");
		}


		//reset
		[Route("ResetPass")]
		public IActionResult ResetPass()
		{
			return View("ResetPass");
		}




		//Send to Email
		[NonAction]
		public void SendOtpToEmail(string email, string emailFor)
		{

			var fromEmail = new MailAddress("****************@gmail.com", "ADMIN from FFORUM");   //enter admin mail
			var toEmail = new MailAddress(email);
			var fromEmailPassword = "**** **** **** ****"; // Replace with actual password

			string subject = "";
			string body = "";
			int otpvalue = 0;
			if (emailFor == "ResetPassword")
			{
				Random rand = new Random();
				otpvalue = rand.Next(999999);
				subject = "Reset Password";
				body = "Hi,<br/>br/>We got request for reset your account password. Please enter otp to reset your password" +
					"<br/><br/><p>" + otpvalue + "</p>";
			}

			contxt.HttpContext.Session.SetInt32("otp", otpvalue);
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


