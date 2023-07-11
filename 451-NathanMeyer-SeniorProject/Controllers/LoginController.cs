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
            // display to the user the login form
            return View();
        }

        // method: ProcessLogin. This method processes the information the user input and checks if it is valid. The application will only accept valid login credentials.
        public IActionResult ProcessLogin(UserModel user)
        {
            // check if the user credentials are valid
            if (security.FindUser(user))
            {
                // if the user credentials are valid, record the user's username and user id into the browser's session variables
                HttpContext.Session.SetString("username", user.Username);
                HttpContext.Session.SetInt32("userID", security.GetUser(user).UserId);

                // take the user to the login success page where they can continue to the game
                return View("LoginSuccess", user);
            }
            else {
                // if the user credentials are not valid, remove any usernames or userIDs from the browser's session variables
                HttpContext.Session.Remove("username");
                HttpContext.Session.Remove("userID");

                // take the user to the login failure page where they can return to the login page to try again
                return View("LoginFailure"); }
        }
    }
}
