using _451_NathanMeyer_SeniorProject.Models;
using _451_NathanMeyer_SeniorProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _451_NathanMeyer_SeniorProject.Controllers
{
    // class: RegisterController. This controller manages the pages that allow the user to register a new account in the application
    public class RegisterController : Controller
    {
        // Security service class
        SecurityDAO security = new SecurityDAO();

        // method: Index. This method provides the user with a form to register an account in the application. The user must enter a unique username and a password to continue.
        public IActionResult Index()
        {
            return View();
        }

        // method: ProcessRegister. This method processes the user information entered in the registration form and uses it to create a new user in the MySQL database.
        public IActionResult ProcessRegister(UserModel user)
        {
            if(security.RegisterUser(user))
            {
                return View("RegistrationSuccessful", user);
            }
            else
            {
                return View("RegistrationFailed", user);
            }
        }
    }
}
