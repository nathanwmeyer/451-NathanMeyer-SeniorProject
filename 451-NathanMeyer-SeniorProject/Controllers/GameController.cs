using _451_NathanMeyer_SeniorProject.Models;
using _451_NathanMeyer_SeniorProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace _451_NathanMeyer_SeniorProject.Controllers
{
    // class: GameController. This controller manages the Lights Out game through the use of the LightsOutLogicService class.
    public class GameController : Controller
    {
        // List of buttons used for displaying the game board
        static List<ButtonModel> buttons = new List<ButtonModel>();

        // Save Data service used for saving and loading game data
        SaveGameDAO service = new SaveGameDAO();

        // Logic service used for playing the lights out game
        LightsOutLogicService gameLogic = new LightsOutLogicService();

        // message string used for telling the user the rules of the game or declaring that the game is complete.
        String message;

        // method: Index. This method contains the game board and all methods involving the game board return partial views to update the view for this class.
        [CustomAuthorization]
        public IActionResult Index()
        {
            // set the length of the rows for the game board
            ViewBag.RowLength = gameLogic.getRowLength();

            // generate a new board
            buttons = gameLogic.setup();

            // set the initial message
            message = "Are you able to turn off all of the lights? Click on a light to change its state.";

            // display the initial message
            ViewBag.Message = message;

            // return the view of the prepared game board page
            return View("Index", buttons);
        }

        // method: HandleButtonClick. This method changes buttons on the game board based on what button was clicked, represented by the button's id. The partial view containing the button is returned to the application.
        public IActionResult HandleButtonClick(int buttonId)
        {
            
            // change the selected button
            gameLogic.changeButtons(buttons, buttonId);

            // return a partial view containing the button to be updated
            return PartialView("PartialButton", buttons.ElementAt(buttonId));
        }

        // method: UpdateButton. This method returns a partial view of the button requested, represented by the button's id. This method is used to update the appearance of the game board without changing the states of the buttons.
        public IActionResult UpdateButton(int buttonId) {

            // return a partial view of the button to be updated
            return PartialView("PartialButton", buttons.ElementAt(buttonId));
        }

        // method: getSize. This method returns the total number of buttons on the game board.
        public int getSize() { return gameLogic.getGridSize(); }
        
        // method: getMessage. This method returns the text of the message under the game board. The text changes based on the current state of the board.
        public string getMessage() {
            message = "Are you able to turn off all of the lights? Click on a light to change its state.";

            // check if the game is completed
            if (gameLogic.lightsOut(buttons)) {
                message = "All of the lights are out. Thank you";
            }

            return message;
        }

        // method: ViewGames. This method shows the user a list of games that they have saved previously. The user may select a game to load and continue playing.
        [CustomAuthorization]
        public IActionResult ViewGames() {
            // retrieve the user's ID
            int user = (int)HttpContext.Session.GetInt32("userID");
            
            return View(service.FindUserGames(user));
        }

        // method: SaveGame. This method saves the user's current game for them to play later. It also shows the user a list of games that they have saved.
        public IActionResult SaveGame() {
            // get the user's ID
            int user = (int)HttpContext.Session.GetInt32("userID");

            // create a new Saved Game to sent to the MySQL server
            SaveGameDTO saveGame = new SaveGameDTO(0, user, JsonSerializer.Serialize(buttons), DateTime.Now);

            // save game to the MySQL server using the SaveGameDAO service
            service.SaveGame(saveGame);

            // retireve and display a list of the user's saved games
            return View("ViewGames", service.FindUserGames(user));
        }

        // method: LoadGame. This method loads a game that the user saved before. The user is able to resume playing the saved game.
        public IActionResult LoadGame(int id) { 

            // retrieve and prepare the buttons using the SaveGameDAO service
            buttons = JsonSerializer.Deserialize<List<ButtonModel>>(service.LoadGame(id).SaveData);

            // Properly format the board by retrieving the row length.
            ViewBag.rowLength = gameLogic.getRowLength();

            // get the application's message for the current board state
            ViewBag.Message = getMessage();

            // display the prepared game board
            return View("Index", buttons);
        }
    }
}
