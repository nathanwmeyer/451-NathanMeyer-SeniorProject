namespace _451_NathanMeyer_SeniorProject.Models
{
    // class: ButtonModel. This class represents a button in the application's game board
    public class ButtonModel
    {
        public ButtonModel(int buttonId, int state)
        {
            ButtonId = buttonId;
            State = state;
        }

        // ButtonID: the button's id on the board
        public int ButtonId { get; set; }

        // State: the button's state, 1 is on, 0 is off
        public int State { get; set; }
    }
}
