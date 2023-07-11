using _451_NathanMeyer_SeniorProject.Models;
using _451_NathanMeyer_SeniorProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _451_NathanMeyer_SeniorProject.Controllers
{
    // class: SaveGameAPIController. This class is not used by any of the functions of the main application. This class is mainly used for admin access to view and manage saved games.
    [ApiController]
    [Route("games/")]
    public class SaveGameAPIController : Controller
    {
        // Save service class
        SaveGameDAO saveService = new SaveGameDAO();

        // method: Index. This method displays a list of saved games belonging to the owner of the user id input in the url
        [HttpGet("view/{id}")]
        public ActionResult <IEnumerable<SaveGameDTO>> Index(int id)
        {
            // return a json of the saved games belonging to the user id
            return saveService.FindUserGames(id);
        }

        // method: ShowOneSaveGame. This method displays a saved game that possesses the same game id input in the url
        [HttpGet("load/{id}")]
        public ActionResult <SaveGameDTO> ShowOneSaveGame(int id)
        {
            // return a json of the saved game with the requested id
            return saveService.LoadGame(id);
        }

        // method: SaveOneGame. This method saves the game input in the body of the URL request.
        [HttpGet("save")]
        public ActionResult <string> SaveOneGame(SaveGameDTO savegame)
        {
            // attempt to save a game that was stored in the body of the request
            bool saved = saveService.SaveGame(savegame);

            // return a message stating if the save attempt passed or failed
            if (saved) { return "save successful"; } else return "save failed";
        }
    }
}
