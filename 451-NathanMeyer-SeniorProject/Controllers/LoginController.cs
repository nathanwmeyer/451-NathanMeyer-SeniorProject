using _451_NathanMeyer_SeniorProject.Models;
using _451_NathanMeyer_SeniorProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace _451_NathanMeyer_SeniorProject.Controllers
{
    // class: LoginController. This controller manages the pages that allow the user to log in and play the application's game
    public class LoginController : Controller
    {
        // Login service class
        SecurityService security = new SecurityService();

        // method: Index. This method shows the user a login form. The user must input their username and password to continue.
        public IActionResult Index()
        {
            return View();
        }

        // method: ProcessLogin. This method processes the information the user input and checks if it is valid. The application will only accept valid login credentials.
        public IActionResult ProcessLogin(UserModel user)
        {
            if (security.FindUser(user))
            {
                HttpContext.Session.SetString("username", user.Username);
                HttpContext.Session.SetInt32("userID", security.GetUser(user).UserId);
                return View("LoginSuccess", user);
            }
            else {
                HttpContext.Session.Remove("username");
                HttpContext.Session.Remove("userID");
                return View("LoginFailure"); }
        }
    }
}
