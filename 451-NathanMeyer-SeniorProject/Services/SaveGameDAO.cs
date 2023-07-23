using _451_NathanMeyer_SeniorProject.Models;
using System.Data.SqlClient;

namespace _451_NathanMeyer_SeniorProject.Services
{
    // class: SaveGameDAO. This class connects to the MySQL server and manages saved game data.
    public class SaveGameDAO
    {
        // connection string for connecting to the MySQL database
        public string connectionString = @"Database=localdb;Data Source=127.0.0.1:55629;User Id=azure;Password=6#vWHD_$";

        // OLD CONNECTION STRING FOR LOCAL TESTING
        // string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=CST451Database;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        // method: FindUserGames. This method returns a list of games that have a user id matching the requested id.
        public List<SaveGameDTO> FindUserGames(int id)
        {
            // create a list of found games to return to the user
            List<SaveGameDTO> foundGames = new List<SaveGameDTO>();

            // prepare a SQL statement
            string sqlStatement = "SELECT * FROM localdb.SavedGames WHERE UserID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // create a SQL command using the prepared statement
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                // add parameters to the command
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    // open the connection and execute the command
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // retrieve the saved games from the MySQL server
                    while(reader.Read())
                    {
                        foundGames.Add(new SaveGameDTO((int)reader[0], (int)reader[1], (string)reader[2], (DateTime)reader[3]));
                    }
                }
                catch (Exception ex) // catch exceptions during the command execution
                {
                    Console.Write(ex.Message);
                }
            }
            // return the list of found games
            return foundGames;
        }

        // method: LoadGame. This method retrieves a saved game using the saved game's id.
        public SaveGameDTO LoadGame(int id)
        {
            // create a new SaveGame object to return to the user later
            SaveGameDTO foundSave = new SaveGameDTO();

            // prepare a SQL statement
            string sqlStatement = "SELECT * FROM localdb.SavedGames WHERE GameId = @id";

            using (SqlConnection connection = new SqlConnection( connectionString))
            {
                // create a SQL command using the prepared statement
                SqlCommand command = new SqlCommand( sqlStatement, connection);

                // add parameters to the command
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    // open the connection and execute the command
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // retrieve the saved game from the MySQL server
                    while (reader.Read())
                    {
                        foundSave = new SaveGameDTO((int)reader[0], (int)reader[1], (string)reader[2], (DateTime)reader[3]);
                    }
                }
                catch (Exception ex) // catch exceptions during the command execution
                {
                    Console.Write(ex.Message);
                }
            }
            // return the list of found games
            return foundSave;
        }

        // method: SaveGame. This method saves the game data given it to the MySQL database
        public bool SaveGame(SaveGameDTO saveGame)
        {
            bool success = true;

            // prepare a SQL statement
            string sqlStatement = "INSERT INTO localdb.SavedGames (USERID, SAVEDATA, SAVEDATE) VALUES (@userid, @savedata, @savedate)";

            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                // create a SQL command using the prepared statement
                SqlCommand command = new SqlCommand ( sqlStatement, conn);

                // add parameters to the command
                command.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = saveGame.UserId;
                command.Parameters.Add("@savedata", System.Data.SqlDbType.Text).Value = saveGame.SaveData;
                command.Parameters.Add("@savedate", System.Data.SqlDbType.DateTime).Value = saveGame.SaveDate;

                try
                {
                    // open the connection and execute the command
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
                catch(Exception ex) // catch exceptions during the command execution
                {
                    Console.Write(ex.Message);
                    success = false;
                }
            }
            // return whether or not the attempted save was successful
            return success;
        }
    }
}
