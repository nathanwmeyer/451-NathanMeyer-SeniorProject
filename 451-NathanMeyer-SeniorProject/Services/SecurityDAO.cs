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

            // prepare a SQL statement
            string sqlStatement = "SELECT * FROM dbo.Users WHERE USERNAME = @username";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                // create a SQL command using the prepared statement
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                // add parameters to the command
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 255).Value = user.Username;

                try
                {
                    // open the connection and execute the command
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // check if the user exists
                    if (reader.HasRows)
                    {
                        success = true;

                        // retrieve the user's hashed password from the MySQL server
                        while (reader.Read())
                        {
                            hash = (string)reader[2];
                        }

                        // check the hashed password with the input password
                        if (BCrypt.Net.BCrypt.Verify(user.Password, hash))
                        {
                            success = true;

                        }
                        else success = false;
                    }
                }
                catch (Exception ex) // catch exceptions during the command execution
                {
                    Console.Write(ex.Message);
                }
            }
            // return whether or not the attempted login was successful
            return success;
        }

        // method: GetUser. This method checks if submitted user information is valid and returns the complete user information.
        public UserModel GetUser(UserModel user) 
        {
            // create a new User object to return to the user later
            UserModel userModel = new UserModel();

            // prepare a SQL statement
            string sqlStatement = "SELECT * FROM dbo.Users WHERE USERNAME = @username";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // create a SQL command using the prepared statement
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                // add parameters to the command
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 255).Value = user.Username;

                try
                {
                    // open the connection and execute the command
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // retrieve user data from the MySQL server
                    while (reader.Read())
                    {
                        userModel = new UserModel((int)reader[0], (string)reader[1], (string)reader[2]);
                    }

                    // check the hashed password with the input password
                    if (BCrypt.Net.BCrypt.Verify(user.Password, userModel.Password))
                    { }
                    else throw new Exception("Something's wrong, the user information somehow changed...");
                }
                catch (Exception ex) // catch exceptions during the command execution
                {
                    Console.Write(ex.Message);
                }
            }
            // return the retrieved user information
            return userModel;
        }

        // method: RegisterUser. This method inserts a new user into the MySQL database. This method encrypts the user password to prevent password leaking.
        public bool RegisterUser(UserModel user) {
            bool success = true;

            // prepare a SQL statement
            string sqlStatement = "INSERT INTO dbo.Users (USERNAME, PASSWORD) VALUES (@username, @password)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // create a SQL command using the prepared statement
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                // add parameters to the command
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 255).Value = user.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 511).Value = BCrypt.Net.BCrypt.HashPassword(user.Password);

                try
                {
                    // open the connection and execute the command
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    
                }
                catch (Exception ex) // catch exceptions during the command execution
                {
                    Console.Write(ex.Message);
                    success = false;
                }
            }
            // return whether or not the attempted registration was successful
            return success;
        }
    }
}
