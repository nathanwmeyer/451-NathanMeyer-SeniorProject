namespace _451_NathanMeyer_SeniorProject.Models
{
    // class: UserModel. This class represents a user in the application. This class is used for logging in users and managing saved game data for the user.
    public class UserModel
    {
        public UserModel()
        {
        }

        // constructor for saving or retrieving user data
        public UserModel(int userId, string username, string password)
        {
            UserId = userId;
            Username = username;
            Password = password;
        }

        // UserId: the user's ID number
        public int UserId { get; set; }

        // Username: the user's selected username. This username must be unique to be saved to the MySQL server
        public string Username { get; set; }

        // Password: the user's password. This password is encrypted using BCrypt.NET-Next
        public string Password { get; set; }
    }
}
