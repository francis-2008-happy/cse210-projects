using System;

namespace ScriptureMemorizer
{
    /*
    * Program: Scripture Memorizer
    * Author: Francis Happy
    * 
    * Description: This program helps users memorize scriptures by gradually
    * hiding words until the entire scripture is hidden.
    * 
    * Exceeding Requirements:
    * 1. Enhanced the program to select only words that aren't already hidden (more efficient)
    * 2. Added color to make the interface more appealing
    * 3. Added confirmation before quitting
    * 4. Added a scripture library that loads from a file
    * 5. Added ability to choose difficulty level (how many words to hide each time)
    * 6. Added a progress indicator showing how much of the scripture is hidden
    */

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Scripture Memorizer Program!");
            
            // Load scripture library
            ScriptureLibrary library = new ScriptureLibrary();
            
            // Get difficulty level
            int wordsToHideEachTime = GetDifficultyLevel();
            
            // Get a random scripture
            Scripture scripture = library.GetRandomScripture();

            // Main program loop
            string input = "";
            while (input.ToLower() != "quit" && !scripture.IsCompletelyHidden())
            {
                // Clear the console
                Console.Clear();
                
                // Display the scripture with color
                DisplayColoredScripture(scripture);
                
                // Show progress
                DisplayProgress(scripture);
                
                Console.WriteLine();
                Console.WriteLine("Press Enter to continue, 'new' for a new scripture, or 'quit' to exit:");
                
                // Get user input
                input = Console.ReadLine();
                
                // Process input
                if (input.ToLower() == "new")
                {
                    // Get a new random scripture
                    scripture = library.GetRandomScripture();
                }
                else if (input.ToLower() == "quit")
                {
                    // Ask for confirmation before quitting
                    Console.WriteLine("Are you sure you want to quit? (yes/no)");
                    string confirmation = Console.ReadLine();
                    if (confirmation.ToLower() != "yes")
                    {
                        input = ""; // Reset input to continue the loop
                    }
                }
                else
                {
                    // Hide random words
                    scripture.HideRandomWords(wordsToHideEachTime);
                }
            }

            // Final display if all words are hidden
            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                DisplayColoredScripture(scripture);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You've memorized the scripture!");
                Console.ResetColor();
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
            }
        }
        
        // Helper method to get difficulty level
        static int GetDifficultyLevel()
        {
            int difficulty = 3; // Default difficulty
            bool validInput = false;
            
            while (!validInput)
            {
                Console.WriteLine("Select difficulty level:");
                Console.WriteLine("1 - Easy (1 word at a time)");
                Console.WriteLine("2 - Medium (3 words at a time)");
                Console.WriteLine("3 - Hard (5 words at a time)");
                
                string input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        difficulty = 1;
                        validInput = true;
                        break;
                    case "2":
                        difficulty = 3;
                        validInput = true;
                        break;
                    case "3":
                        difficulty = 5;
                        validInput = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
            
            return difficulty;
        }
        
        // Helper method to display the scripture with color
        static void DisplayColoredScripture(Scripture scripture)
        {
            // Get the display text from the scripture
            string text = scripture.GetDisplayText();
            
            // Split the text into reference and content parts
            string[] parts = text.Split('-');
            
            if (parts.Length >= 2)
            {
                // Display the reference in yellow
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(parts[0].Trim());
                Console.ResetColor();
                
                Console.Write(" - ");
                
                // Display the scripture text in white
                Console.WriteLine(parts[1].Trim());
            }
            else
            {
                // Fallback if splitting doesn't work
                Console.WriteLine(text);
            }
        }
        
        // Helper method to display progress
        static void DisplayProgress(Scripture scripture)
        {
            // Get percentage of words hidden
            int percentHidden = scripture.GetPercentHidden();
            
            Console.Write("Progress: [");
            
            // Draw a progress bar
            for (int i = 0; i < 20; i++)
            {
                if (i < percentHidden / 5)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("█");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            
            Console.ResetColor();
            Console.WriteLine($"] {percentHidden}% memorized");
        }
    }
}