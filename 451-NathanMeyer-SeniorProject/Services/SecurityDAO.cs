using _451_NathanMeyer_SeniorProject.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Policy;

namespace _451_NathanMeyer_SeniorProject.Services
{
    // class: SecurityDAO. This class connects to the MySQL database and manages User information.
    public class SecurityDAO
    {
        // connection string for connecting to the MySQL database
        public string ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=CST451Database;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        public SecurityDAO() { }

        // method: FindUser. This method checks to see if user information is valid. This method will only return true if the user 
        public bool FindUser(UserModel user)
        {
            bool success = false;
            string hash = "";

            string sqlStatement = "SELECT * FROM dbo.Users WHERE USERNAME = @username";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 255).Value = user.Username;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;

                        while (reader.Read())
                        {
                            hash = (string)reader[2];
                        }
                        if (BCrypt.Net.BCrypt.Verify(user.Password, hash))
                        {
                            success = true;

                        }
                        else success = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return success;
        }

        // method: GetUser. This method checks if submitted user information is valid and returns the complete user information.
        public UserModel GetUser(UserModel user) 
        {
            UserModel userModel = new UserModel();

            string sqlStatement = "SELECT * FROM dbo.Users WHERE USERNAME = @username";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 255).Value = user.Username;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        userModel = new UserModel((int)reader[0], (string)reader[1], (string)reader[2]);
                    }

                    if (BCrypt.Net.BCrypt.Verify(user.Password, userModel.Password))
                    { }
                    else throw new Exception("Something's wrong, the user information somehow changed...");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return userModel;
        }

        // method: RegisterUser. This method inserts a new user into the MySQL database. This method encrypts the user password to prevent password leaking.
        public bool RegisterUser(UserModel user) {
            bool success = true;

            string sqlStatement = "INSERT INTO dbo.Users (USERNAME, PASSWORD) VALUES (@username, @password)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 255).Value = user.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 511).Value = BCrypt.Net.BCrypt.HashPassword(user.Password);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    success = false;
                }
            }

            return success;
        }
    }
}
