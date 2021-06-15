using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroundColorManager :IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
   

        public BackgroundColorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Preferences Menu");
            Console.WriteLine(" 1) Change Background Color");
            Console.WriteLine(" 2) Reset Background Color");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ChangeBackground();
                    
                case "2":
                    Reset();
                    return _parentUI;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Reset()
        {
            Console.ResetColor();
            Console.WriteLine("\nOriginal colors restored...");
        }

        private void ChangeBackground()
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            ConsoleColor currentBackground = Console.BackgroundColor;
            Console.WriteLine("All the foreground colors except {0}, the background color:",
                        currentBackground);
            int i = 0;
            foreach (var color in colors)
            {
                if (color == currentBackground) continue;
                i++;
                Console.ForegroundColor = color;
                Console.WriteLine($"{i}) {color}");
            }
        } 
    }
}
