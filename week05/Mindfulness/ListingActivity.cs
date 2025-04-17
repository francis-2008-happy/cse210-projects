using System;
using System.Collections.Generic;
using System.Threading;

class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };
    
    private List<int> _usedPromptIndexes = new List<int>();

    public ListingActivity()
    {
        _name = "Listing Activity";
        _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    protected override void PerformActivity()
    {
        Random random = new Random();
        
        // Get a unique prompt (not used in this session)
        int promptIndex;
        if (_usedPromptIndexes.Count >= _prompts.Count)
        {
            _usedPromptIndexes.Clear(); // Reset if all prompts have been used
        }
        
        do {
            promptIndex = random.Next(_prompts.Count);
        } while (_usedPromptIndexes.Contains(promptIndex));
        
        _usedPromptIndexes.Add(promptIndex);
        string prompt = _prompts[promptIndex];
        
        Console.WriteLine(prompt);
        Console.Write("You may begin in: ");
        ShowCountdown(5);

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        Console.WriteLine("Start listing items:");
        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                items.Add(item);
            }
        }

        Console.WriteLine($"You listed {items.Count} items.");
    }
}