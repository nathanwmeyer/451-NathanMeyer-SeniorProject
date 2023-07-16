using _451_NathanMeyer_SeniorProject.Models;

namespace _451_NathanMeyer_SeniorProject.Services
{
    // class: LightsOutLogicService. This class manages the actions used during the Lights Out game. This class changes the states of the buttons on the form and declares when the game has finished.
    public class LightsOutLogicService
    {
        Random random = new Random(); // Random variable used for board creation
        int GRID_SIZE = 25; // Preset size of the board in total number of buttons
        int ROW_LENGTH = 5; // Preset size of each row
        List<ButtonModel> buttons = new List<ButtonModel>(); // Game Board loaded during each game

        // method: setup. This method creates a new game for the user to play.
        public List<ButtonModel> setup() {
            bool success = false;
            while (!success)
            {
                success = true;
                // for each button in the game board
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    // set the button to the on position about 25% of the time and add it to the button list
                    int newState = 0;
                    if (random.Next(4) == 0) newState = 1;
                    buttons.Add(new ButtonModel(i, newState));
                }
                success = isSolvable(buttons);
                if (!success) { buttons.Clear();} // wipe the board if it's not solvable
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

            // get the information about the surrounding buttons
            // is the button on the left a valid button to change?
            bool clearLeft = buttonId % ROW_LENGTH > 0; // it will only be valid if this button is NOT the first button
            
            // is the button on the right a valid button to change?
            bool clearRight = buttonId % ROW_LENGTH != ROW_LENGTH -1; // it will only be valid if this button is NOT the last button

            // is the button above this button a valid button to change?
            bool clearTop = buttonId > ROW_LENGTH - 1; // it will only be valid if this button is NOT on the first row

            // is the button below this button a valid button to change?
            bool clearBottom = buttonId < GRID_SIZE - ROW_LENGTH; // it will only be valid if this button is NOT on the last row.

            //change the buttons that are valid
            if (clearLeft) { buttons.ElementAt(buttonId - 1).State = (buttons.ElementAt(buttonId - 1).State + 1) % 2; }
            if (clearRight) { buttons.ElementAt(buttonId + 1).State = (buttons.ElementAt(buttonId + 1).State + 1) % 2; }
            if (clearTop) { buttons.ElementAt(buttonId - ROW_LENGTH).State = (buttons.ElementAt(buttonId - ROW_LENGTH).State + 1) % 2; }
            if (clearBottom) { buttons.ElementAt(buttonId + ROW_LENGTH).State = (buttons.ElementAt(buttonId + ROW_LENGTH).State + 1) % 2; }


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

        // method: isSolvable. This method checks if the current game board is solvable. Checks if the board is solvable.
        public bool isSolvable(List<ButtonModel> buttons) {
            int x = 0;
            int y = 0;
                for (int i = 0; i < buttons.Count; i++) // check through the entire grid in a pattern
                {
                if (i == 0 || i == 2 || i == 4 || i == 5 || i == 7 || i == 9 || i == 15 || i == 17 || i == 19 || i == 20 || i == 22 || i == 24) // check these buttons
                    {
                        if (buttons[i].State == 1) { x++; } // count the number of lit buttons that weren't ignored

                    }
                }
                Console.WriteLine("Buttons on x: " + (x % 2 == 0)); // confirm in the console that the number of buttons counted is even
                if (x % 2 != 0) return false; // return false if the number of buttons counted was odd

                for (int i = 0; i < buttons.Count; i++) // check through the entire grid in a pattern
            {
                    if (i == 0 || i == 1 || i == 3 || i == 4 || i == 10 || i == 11 || i == 13 || i == 14 || i == 20 || i == 21 || i == 23 || i == 24) // check these buttons
                    {
                        if (buttons[i].State == 1) { y++; } // count the number of lit buttons that weren't ignored

                    }
                }
                Console.WriteLine("Buttons on y: " + (y % 2 == 0)); // confirm in the console that the number of buttons counted is even
            if (y % 2 != 0) return false;
            Console.WriteLine("^ Generated at: " + DateTime.Now); // if we got to this point in the code, both tests must have passed.
            return true; // if both tests have passed, the puzzle is solvable. Return true
        }

        // method: getGridSize. This method returns the size of the button grid in the form of the total number of buttons.
        public int getGridSize() { return GRID_SIZE; }

        // method: getRowLength. This method returns the length of the button grid rows. This is used to properly format the button grid
        public int getRowLength() {  return ROW_LENGTH; }
    }
}
