using Microsoft.AspNetCore.Mvc;
using TextGame.Models;
using System.Collections.Generic;

namespace TextGame.Controllers
{
    public class TextGameController : Controller
    {
        private static readonly Dictionary<string, string[]> KeyMap = new()
        {
            { "2", new[] { "A", "B", "C" } },
            { "3", new[] { "D", "E", "F" } },
            { "4", new[] { "G", "H", "I" } },
            { "5", new[] { "J", "K", "L" } },
            { "6", new[] { "M", "N", "O" } },
            { "7", new[] { "P", "Q", "R", "S" } },
            { "8", new[] { "T", "U", "V" } },
            { "9", new[] { "W", "X", "Y", "Z" } },
            { "0", new[] { " " } },
        };

        private readonly List<string> storyLines = new()
        {
            "You receive a text from an unknown number: 'Do you want to play a game?'",
            "1. Yes  2. No",
            "You reply 'Yes'. The screen goes black… Suddenly you hear a whisper: 'I see you.'",
            "1. Reply 'Who is this?'  2. Stay silent",
            "A shadow moves behind you. Your phone vibrates: 'Don't look back.'",
            "You stay silent. The whisper grows louder: 'Why won't you play?'",
            "You ignore the text. Your phone buzzes again: 'You can't escape me.'",
            "1. Delete the number  2. Call the number",
            "You delete the number. Suddenly all the lights go out. The text reads: 'Too late.'",
            "You call. A distorted voice whispers: 'Finally…' Your phone explodes in your hand."
        };

        [HttpGet]
        public IActionResult Index()
        {
            var model = new TextGameViewModel
            {
                DisplayText = storyLines[0],
                StoryLines = storyLines
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult PressButton([FromForm] TextGameViewModel model, string key)
        {
            if (key == "*")
            {
                model.CurrentText = "";
                model.LastButton = "";
                model.PressCount = 0;
            }
            else if (key == "#")
            {
                model.CurrentText += GetChar(model.LastButton, model.PressCount);
                model.LastButton = "";
                model.PressCount = 0;
            }
            else
            {
                if (model.LastButton == key)
                    model.PressCount++;
                else
                {
                    if (!string.IsNullOrEmpty(model.LastButton))
                        model.CurrentText += GetChar(model.LastButton, model.PressCount);

                    model.LastButton = key;
                    model.PressCount = 1;
                }
            }

            string preview = GetChar(model.LastButton, model.PressCount);
            model.DisplayText = model.CurrentText + preview;

            // Advance story on "0"
            if (key == "0" && model.StoryIndex < storyLines.Count - 1)
            {
                model.StoryIndex++;
                model.DisplayText = storyLines[model.StoryIndex];
            }

            return View("Index", model);
        }

        private string GetChar(string key, int pressCount)
        {
            if (string.IsNullOrEmpty(key) || !KeyMap.ContainsKey(key))
                return "";

            var chars = KeyMap[key];
            int index = (pressCount - 1) % chars.Length;
            return chars[index];
        }
    }
}
