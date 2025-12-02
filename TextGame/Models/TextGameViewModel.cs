using System.Collections.Generic;

namespace TextGame.Models
{
    public class TextGameViewModel
    {
        public string DisplayText { get; set; } = "";
        public string CurrentText { get; set; } = "";
        public string LastButton { get; set; } = "";
        public int PressCount { get; set; } = 0;
        public int StoryIndex { get; set; } = 0;
        public List<string> StoryLines { get; set; } = new List<string>();
    }
}
