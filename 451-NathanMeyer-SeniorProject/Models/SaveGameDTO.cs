using System.ComponentModel;

namespace _451_NathanMeyer_SeniorProject.Models
{
    // class: SaveGameDTO. This class represents a saved game. This class is used for saving new games to the MySQL server or retrieving a saved game from the MySQL server.
    public class SaveGameDTO
    {
        public SaveGameDTO()
        {
        }

        public SaveGameDTO(int id, int userId, string saveData, DateTime saveDate)
        {
            Id = id;
            UserId = userId;
            SaveData = saveData;
            SaveDate = saveDate;
        }

        [DisplayName("Game Id")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SaveData { get; set; }

        [DisplayName("Save Date")]
        public DateTime SaveDate { get; set; }
    }
}
