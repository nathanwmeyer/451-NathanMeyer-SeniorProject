using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _451_NathanMeyer_SeniorProject.Controllers
{
    // class: CustomAuthorizationAttribute. This class is an attribute that can be included in class methods to prevent a user from accessing certain parts of the application without authorization.
    internal class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        // method: OnAuthorization. This method checks if the user is logged in and redirects the user to the login page if they are not logged in
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string username = context.HttpContext.Session.GetString("username");
            if (username == null) { context.Result = new RedirectResult("/login"); }
            else { }
        }
    }
}
