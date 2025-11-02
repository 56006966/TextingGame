using Microsoft.AspNetCore.Mvc;
using TextGame.Models;

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

        [HttpGet]
        public IActionResult Index()
        {
            return View(new TextGameViewModel());
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
                {
                    model.PressCount++;
                }
                else
                {   
                    if (!string.IsNullOrEmpty(model.LastButton))
                    {
                        model.CurrentText += GetChar(model.LastButton, model.PressCount);
                    }

                    model.LastButton = key;
                    model.PressCount = 1;
                }
       

            string preview = GetChar(model.LastButton, model.PressCount);
            model.DisplayText = model.CurrentText + preview;

            return View("Index", model);
        }


        var currentChar = GetChar(model.LastButton, model.PressCount);
            model.DisplayText = model.CurrentText + currentChar;

            return View("Index", model);
        }

        private string GetChar(string key, int pressCount)
        {
            if (string.IsNullOrEmpty(key) || !KeyMap.ContainsKey(key)) return "";

            var chars = KeyMap[key];
            int index = (pressCount - 1) % chars.Length;
            return chars[index];
        }
    }
}
