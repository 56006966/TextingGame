namespace TextGame.Models
{
    public class TextGameViewModel
    {
        public string CurrentText { get; set; } = "";
        public string LastButton { get; set; } = "";
        public int PressCount { get; set; } = 0;
        public string DisplayText { get; set; } = "";
        public DateTime LastPressTime { get; set; }

    }
}
