namespace _451_NathanMeyer_SeniorProject.Models
{
    // class: UserModel. This class represents a user in the application. This class is used for logging in users and managing saved game data for the user.
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(int userId, string username, string password)
        {
            UserId = userId;
            Username = username;
            Password = password;
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
