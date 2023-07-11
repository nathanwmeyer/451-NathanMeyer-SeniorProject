using _451_NathanMeyer_SeniorProject.Models;

namespace _451_NathanMeyer_SeniorProject.Services
{
    // class: SecurityService. This class calls the SecurityDAO for the rest of the application. All requests to the SecurityDAO go through this class.
    public class SecurityService
    {
        // SecurityDAO instance
        SecurityDAO securityDAO = new SecurityDAO();

        // method: FindUser. This method calls the SecurityDAO FindUser method to find a user in the MySQL database.
        public bool FindUser(UserModel user)
        {
            return securityDAO.FindUser(user);
        }

        // method: GetUser. This method calls the SecurityDAO GetUser method to retrieve user information from the MySQL database.
        public UserModel GetUser(UserModel user) { 
            return securityDAO.GetUser(user);
        }

        // method: RegisterUser. This method calls the SecurityDAO RegisterUser method to save a new user into the MySQL database.
        public bool RegisterUser(UserModel user) { 
            return securityDAO.RegisterUser(user);
        }
    }
}
