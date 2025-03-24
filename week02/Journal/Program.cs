using System;
using System.Collections.Generic;
using System.IO;

// Main program class
class Program
{
    static void Main(string[] args)
    {
        // Create journal and prompter instances
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        bool quit = false;
        
        while (!quit)
        {
            // Display the menu
            Console.WriteLine("\n===== Journal Program =====");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Select an option (1-5): ");
            
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    // Write a new entry
                    string prompt = promptGenerator.GetRandomPrompt();
                    Console.WriteLine($"Prompt: {prompt}");
                    Console.Write("> ");
                    string response = Console.ReadLine();
                    
                    Entry newEntry = new Entry
                    {
                        Date = DateTime.Now.ToString("MM/dd/yyyy"),
                        Prompt = prompt,
                        Response = response
                    };
                    
                    journal.AddEntry(newEntry);
                    Console.WriteLine("Entry added successfully!");
                    break;
                    
                case "2":
                    // Display the journal
                    journal.DisplayEntries();
                    break;
                    
                case "3":
                    // Save the journal to a file
                    Console.Write("Enter filename to save: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                    
                case "4":
                    // Load the journal from a file
                    Console.Write("Enter filename to load: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                    
                case "5":
                    // Quit the program
                    quit = true;
                    break;
                    
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}

// Class representing a journal entry
class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    
    public void Display()
    {
        Console.WriteLine($"Date: {Date}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}");
        Console.WriteLine();
    }
}

// Class for managing journal entries
class Journal
{
    private List<Entry> _entries = new List<Entry>();
    
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }
    
    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries in journal.");
            return;
        }
        
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }
    
    public void SaveToFile(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in _entries)
                {
                    // Using ~|~ as a delimiter to avoid issues with commas in content
                    writer.WriteLine($"{entry.Date}~|~{entry.Prompt}~|~{entry.Response}");
                }
            }
            Console.WriteLine($"Journal saved to {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}");
        }
    }
    
    public void LoadFromFile(string filename)
    {
        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File {filename} not found.");
                return;
            }
            
            _entries.Clear();
            
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split("~|~");
                    
                    if (parts.Length == 3)
                    {
                        Entry entry = new Entry
                        {
                            Date = parts[0],
                            Prompt = parts[1],
                            Response = parts[2]
                        };
                        
                        _entries.Add(entry);
                    }
                }
            }
            
            Console.WriteLine($"Journal loaded from {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
        }
    }
}

// Class for generating prompts
class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is something new I learned today?",
        "What challenge did I overcome today?",
        "What made me smile today?",
        "What am I grateful for today?",
        "What goal did I make progress on today?"
    };
    
    private Random _random = new Random();
    
    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}

/* 
Enhancements beyond requirements:
1. Added 5 extra prompts beyond the required 5 to give users more variety
2. Implemented error handling for file operations to prevent crashes
3. Added validation to ensure loaded entries have the correct format
4. Used DateTime.Now to automatically get the current date for entries
5. Added clear feedback messages to help users understand what's happening
*/