using _451_NathanMeyer_SeniorProject.Models;
using System.Data.SqlClient;

namespace _451_NathanMeyer_SeniorProject.Services
{
    // class: SaveGameDAO. This class connects to the MySQL server and manages saved game data.
    public class SaveGameDAO
    {
        // connection string for connecting to the MySQL database
        string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=CST451Database;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        // method: FindUserGames. This method returns a list of games that have a user id matching the requested id.
        public List<SaveGameDTO> FindUserGames(int id)
        {
            List<SaveGameDTO> foundGames = new List<SaveGameDTO>();

            string sqlStatement = "SELECT * FROM dbo.SavedGames WHERE UserID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        foundGames.Add(new SaveGameDTO((int)reader[0], (int)reader[1], (string)reader[2], (DateTime)reader[3]));
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return foundGames;
        }

        // method: LoadGame. This method retrieves a saved game using the saved game's id.
        public SaveGameDTO LoadGame(int id)
        {
            SaveGameDTO foundSave = new SaveGameDTO();

            string sqlStatement = "SELECT * FROM dbo.SavedGames WHERE GameId = @id";

            using (SqlConnection connection = new SqlConnection( connectionString))
            {
                SqlCommand command = new SqlCommand( sqlStatement, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundSave = new SaveGameDTO((int)reader[0], (int)reader[1], (string)reader[2], (DateTime)reader[3]);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            return foundSave;
        }

        // method: SaveGame. This method saves the game data given it to the MySQL database
        public bool SaveGame(SaveGameDTO saveGame)
        {
            bool success = true;

            string sqlStatement = "INSERT INTO dbo.SavedGames (USERID, SAVEDATA, SAVEDATE) VALUES (@userid, @savedata, @savedate)";

            using (SqlConnection conn = new SqlConnection(connectionString)) 
            { 
                SqlCommand command = new SqlCommand ( sqlStatement, conn);

                command.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = saveGame.UserId;
                command.Parameters.Add("@savedata", System.Data.SqlDbType.Text).Value = saveGame.SaveData;
                command.Parameters.Add("@savedate", System.Data.SqlDbType.DateTime).Value = saveGame.SaveDate;

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                    success = false;
                }
            }
            return success;
        }
    }
}
