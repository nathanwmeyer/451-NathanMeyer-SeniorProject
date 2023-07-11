using System.ComponentModel;

namespace _451_NathanMeyer_SeniorProject.Models
{
    // class: SaveGameDTO. This class represents a saved game. This class is used for saving new games to the MySQL server or retrieving a saved game from the MySQL server.
    public class SaveGameDTO
    {
        public SaveGameDTO()
        {
        }

        // constructor for creating or retrieving saved games
        public SaveGameDTO(int id, int userId, string saveData, DateTime saveDate)
        {
            Id = id;
            UserId = userId;
            SaveData = saveData;
            SaveDate = saveDate;
        }

        // Id: the game's ID number
        [DisplayName("Game ID")]
        public int Id { get; set; }

        // UserId: the ID number belonging to the user who saved the game
        public int UserId { get; set; }

        // SaveData: a JSON serialized string containing the data of the board state when the user saved
        public string SaveData { get; set; }

        // SaveDate: a DateTime variable of when the user saved the game
        [DisplayName("Save Date")]
        public DateTime SaveDate { get; set; }
    }
}
