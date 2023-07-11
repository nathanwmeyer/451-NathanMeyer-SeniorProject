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

        public int ButtonId { get; set; }
        public int State { get; set; }
    }
}
