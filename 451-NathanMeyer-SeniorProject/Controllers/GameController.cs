using _451_NathanMeyer_SeniorProject.Models;
using _451_NathanMeyer_SeniorProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace _451_NathanMeyer_SeniorProject.Controllers
{
    // class: GameController. This controller manages the Lights Out game through the use of the LightsOutLogicService class.
    public class GameController : Controller
    {
        static List<ButtonModel> buttons = new List<ButtonModel>();

        SaveGameDAO service = new SaveGameDAO();

        LightsOutLogicService gameLogic = new LightsOutLogicService();

        String message;;

        // method: Index. This method contains the game board and all methods involving the game board return partial views to update the view for this class.
        [CustomAuthorization]
        public IActionResult Index()
        {
            ViewBag.RowLength = gameLogic.getRowLength();

            buttons = gameLogic.setup();

            message = "Are you able to turn off all of the lights? Click on a light to change its state.";

            ViewBag.Message = message;
            return View("Index", buttons);
        }

        // method: HandleButtonClick. This method changes buttons on the game board based on what button was clicked, represented by the button's id. The partial view containing the button is returned to the application.
        public IActionResult HandleButtonClick(int buttonId)
        {
            Console.WriteLine("ButtonsCount: " + buttons.Count());
            gameLogic.changeButtons(buttons, buttonId);
            Console.WriteLine("changed: " + buttonId);
            return PartialView("PartialButton", buttons.ElementAt(buttonId));
        }

        // method: UpdateButton. This method returns a partial view of the button requested, represented by the button's id. This method is used to update the appearance of the game board without changing the states of the buttons.
        public IActionResult UpdateButton(int buttonId) {
            Console.WriteLine("updating: " + buttonId);
            return PartialView("PartialButton", buttons.ElementAt(buttonId));
        }

        // method: getSize. This method returns the total number of buttons on the game board.
        public int getSize() { return gameLogic.getGridSize(); }
        
        // method: getMessage. This method returns the text of the message under the game board. The text changes based on the current state of the board.
        public string getMessage() {
            message = "Are you able to turn off all of the lights? Click on a light to change its state.";
            if (gameLogic.lightsOut(buttons)) {
                message = "All of the lights are out. Thank you";
            }

            return message;
        }

        // method: ViewGames. This method shows the user a list of games that they have saved previously. The user may select a game to load and continue playing.
        [CustomAuthorization]
        public IActionResult ViewGames() {
            int user = (int)HttpContext.Session.GetInt32("userID");
            
            return View(service.FindUserGames(user));
        }

        // method: SaveGame. This method saves the user's current game for them to play later. It also shows the user a list of games that they have saved.
        public IActionResult SaveGame() {
            int user = (int)HttpContext.Session.GetInt32("userID");
            SaveGameDTO saveGame = new SaveGameDTO(0, user, JsonSerializer.Serialize(buttons), DateTime.Now);
            service.SaveGame(saveGame);
            return View("ViewGames", service.FindUserGames(user));
        }

        // method: LoadGame. This method loads a game that the user saved before. The user is able to resume playing the saved game.
        public IActionResult LoadGame(int id) { 
            buttons = JsonSerializer.Deserialize<List<ButtonModel>>(service.LoadGame(id).SaveData);
            ViewBag.rowLength = gameLogic.getRowLength();
            ViewBag.Message = getMessage();
            return View("Index", buttons);
        }
    }
}
