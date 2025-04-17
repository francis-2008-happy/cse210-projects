using System;
using System.Collections.Generic;
using System.IO;

// Class to manage the quest system
public class QuestManager
{
    private List<Goal> _goals;
    private int _score;
    private int _level;
    private Dictionary<int, string> _achievementTitles;
    
    // Constructor
    public QuestManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _level = 1;
        InitializeAchievementTitles();
    }
    
    // Initialize the achievement titles for different levels (a creative addition)
    private void InitializeAchievementTitles()
    {
        _achievementTitles = new Dictionary<int, string>
        {
            { 1, "Novice Quester" },
            { 2, "Apprentice Achiever" },
            { 3, "Determined Dreamer" },
            { 4, "Goal Getter" },
            { 5, "Magnificent Milestone Maker" },
            { 6, "Purposeful Pilgrim" },
            { 7, "Visionary Voyager" },
            { 8, "Tenacious Trailblazer" },
            { 9, "Enlightened Explorer" },
            { 10, "Eternal Quest Master" }
        };
    }
    
    // Add a new goal
    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }
    
    // Record event for a goal
    public void RecordEvent(int index)
    {
        if (index >= 0 && index < _goals.Count)
        {
            int pointsEarned = _goals[index].RecordEvent();
            _score += pointsEarned;
            
            Console.WriteLine($"Congratulations! You earned {pointsEarned} points!");
            
            // Check if level up occurred
            int oldLevel = _level;
            UpdateLevel();
            
            if (_level > oldLevel)
            {
                Console.WriteLine($"\n*** LEVEL UP! ***");
                Console.WriteLine($"You are now a Level {_level} {_achievementTitles[_level]}!");
            }
        }
    }
    
    // Update the level based on score
    private void UpdateLevel()
    {
        // Define level thresholds
        int[] levelThresholds = { 0, 1000, 2500, 5000, 10000, 20000, 35000, 50000, 75000, 100000 };
        
        for (int i = levelThresholds.Length - 1; i >= 0; i--)
        {
            if (_score >= levelThresholds[i])
            {
                _level = i + 1;
                break;
            }
        }
    }
    
    // Get the current score
    public int GetScore()
    {
        return _score;
    }
    
    // Get the current level and title
    public string GetLevelInfo()
    {
        return $"Level {_level}: {_achievementTitles[_level]}";
    }
    
    // List all goals
    public void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals yet.");
            return;
        }
        
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }
    
    // Save goals and score to file
    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            // First line is the score
            outputFile.WriteLine(_score);
            
            // Write each goal on a new line
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
        
        Console.WriteLine($"Goals and progress saved to {filename}");
    }
    
    // Load goals and score from file
    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            // Clear current goals
            _goals.Clear();
            
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filename);
            
            if (lines.Length > 0)
            {
                // First line is the score
                _score = int.Parse(lines[0]);
                UpdateLevel();
                
                // Read each goal
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] parts = line.Split(":", 2);
                    
                    if (parts.Length == 2)
                    {
                        string goalType = parts[0];
                        string goalData = parts[1];
                        string[] goalParts = goalData.Split(",");
                        
                        // Create the appropriate goal type
                        switch (goalType)
                        {
                            case "SimpleGoal":
                                if (goalParts.Length >= 4)
                                {
                                    SimpleGoal simpleGoal = new SimpleGoal(
                                        goalParts[0],
                                        goalParts[1],
                                        int.Parse(goalParts[2]),
                                        bool.Parse(goalParts[3])
                                    );
                                    _goals.Add(simpleGoal);
                                }
                                break;
                                
                            case "EternalGoal":
                                if (goalParts.Length >= 3)
                                {
                                    EternalGoal eternalGoal = new EternalGoal(
                                        goalParts[0],
                                        goalParts[1],
                                        int.Parse(goalParts[2])
                                    );
                                    _goals.Add(eternalGoal);
                                }
                                break;
                                
                            case "ChecklistGoal":
                                if (goalParts.Length >= 6)
                                {
                                    ChecklistGoal checklistGoal = new ChecklistGoal(
                                        goalParts[0],
                                        goalParts[1],
                                        int.Parse(goalParts[2]),
                                        int.Parse(goalParts[3]),
                                        int.Parse(goalParts[4]),
                                        int.Parse(goalParts[5])
                                    );
                                    _goals.Add(checklistGoal);
                                }
                                break;
                        }
                    }
                }
            }
            
            Console.WriteLine($"Goals and progress loaded from {filename}");
        }
        else
        {
            Console.WriteLine("File not found. No goals were loaded.");
        }
    }
}