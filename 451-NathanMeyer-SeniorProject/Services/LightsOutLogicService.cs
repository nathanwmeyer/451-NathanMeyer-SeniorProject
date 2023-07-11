using _451_NathanMeyer_SeniorProject.Models;

namespace _451_NathanMeyer_SeniorProject.Services
{
    // class: LightsOutLogicService. This class manages the actions used during the Lights Out game. This class changes the states of the buttons on the form and declares when the game has finished.
    public class LightsOutLogicService
    {
        Random random = new Random(); // Random variable used for board creation
        int GRID_SIZE = 30; // Preset size of the board in total number of buttons
        int ROW_LENGTH = 5; // Preset size of each row
        List<ButtonModel> buttons = new List<ButtonModel>(); // Game Board loaded during each game

        // method: setup. This method creates a new game for the user to play.
        public List<ButtonModel> setup() { 

            // for each button in the game board
            for (int i=0; i <GRID_SIZE; i++)
            {
                // set the button to the on position about 25% of the time and add it to the button list
                int newState = 0;
                if (random.Next(4) == 0) newState = 1;
                buttons.Add(new ButtonModel(i, newState));
            }
            // return the completed list
            return buttons;
        }

        // method: changeButtons. This method changes the button board based on the button clicked. This method returns the updated list of buttons.
        public List<ButtonModel> changeButtons(List<ButtonModel> buttons, int buttonId) {
            // retrieve the current value of the selected button
            int value = buttons.ElementAt(buttonId).State;

            // change the button's state
            buttons.ElementAt(buttonId).State = (value + 1) % 2;

            // return the updated list of buttons
            return buttons;
        }

        // method: lightsOut. This method checks if all of the buttons are in the "off" state. Returns true if all buttons are in the "off" state.
        public bool lightsOut(List<ButtonModel> buttons) {
            bool finished = true;

            // for each button in the button grid, check if each button has a state of 1. If there is a button with a state of 1, return false
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].State == 1) { finished = false; }
            }
            return finished;
        }

        // method: isSolvable. This method checks if the current game board is solvable. Currently always returns true but will check if the board is solvable.
        public bool isSolvable(List<ButtonModel> buttons) { return true; }

        // method: getGridSize. This method returns the size of the button grid in the form of the total number of buttons.
        public int getGridSize() { return GRID_SIZE; }

        // method: getRowLength. This method returns the length of the button grid rows. This is used to properly format the button grid
        public int getRowLength() {  return ROW_LENGTH; }
    }
}
