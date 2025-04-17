using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a new quest manager
        QuestManager questManager = new QuestManager();
        
        bool running = true;
        
        while (running)
        {
            // Display the menu
            DisplayMenu(questManager);
            
            // Get the user's choice
            Console.Write("\nSelect a choice from the menu: ");
            string input = Console.ReadLine();
            
            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        // Create a new goal
                        CreateGoal(questManager);
                        break;
                    case 2:
                        // List goals
                        questManager.ListGoals();
                        PressEnterToContinue();
                        break;
                    case 3:
                        // Save goals
                        SaveGoals(questManager);
                        break;
                    case 4:
                        // Load goals
                        LoadGoals(questManager);
                        break;
                    case 5:
                        // Record event
                        RecordEvent(questManager);
                        break;
                    case 6:
                        // Exit
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a number.");
            }
        }
        
        Console.WriteLine("Thank you for using the Eternal Quest program!");
    }
    
    // Display the menu
    static void DisplayMenu(QuestManager questManager)
    {
        Console.Clear();
        Console.WriteLine("===========================================");
        Console.WriteLine("==        E T E R N A L  Q U E S T       ==");
        Console.WriteLine("===========================================");
        Console.WriteLine($"Score: {questManager.GetScore()} points");
        Console.WriteLine($"{questManager.GetLevelInfo()}");
        Console.WriteLine("===========================================");
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. List Goals");
        Console.WriteLine("3. Save Goals");
        Console.WriteLine("4. Load Goals");
        Console.WriteLine("5. Record Event");
        Console.WriteLine("6. Quit");
        Console.WriteLine("===========================================");
    }
    
    // Create a new goal
    static void CreateGoal(QuestManager questManager)
    {
        Console.Clear();
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Progress Goal");
        
        Console.Write("\nWhich type of goal would you like to create? ");
        string input = Console.ReadLine();
        
        if (!int.TryParse(input, out int goalType) || goalType < 1 || goalType > 4)
        {
            Console.WriteLine("Invalid goal type. Please try again.");
            PressEnterToContinue();
            return;
        }
        
        // Get common goal details
        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        
        Console.Write("What is the amount of points associated with this goal? ");
        if (!int.TryParse(Console.ReadLine(), out int points) || points < 0)
        {
            Console.WriteLine("Invalid points value. Please enter a non-negative number.");
            PressEnterToContinue();
            return;
        }
        
        Goal goal;
        
        switch (goalType)
        {
            case 1: // Simple Goal
                goal = new SimpleGoal(name, description, points);
                break;
                
            case 2: // Eternal Goal
                goal = new EternalGoal(name, description, points);
                break;
                
            case 3: // Checklist Goal
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                if (!int.TryParse(Console.ReadLine(), out int target) || target <= 0)
                {
                    Console.WriteLine("Invalid target value. Please enter a positive number.");
                    PressEnterToContinue();
                    return;
                }
                
                Console.Write("What is the bonus for accomplishing it that many times? ");
                if (!int.TryParse(Console.ReadLine(), out int bonus) || bonus < 0)
                {
                    Console.WriteLine("Invalid bonus value. Please enter a non-negative number.");
                    PressEnterToContinue();
                    return;
                }
                
                goal = new ChecklistGoal(name, description, points, target, bonus);
                break;
                
            case 4: // Progress Goal
                Console.Write("What is the target value to complete this goal? ");
                if (!int.TryParse(Console.ReadLine(), out int targetValue) || targetValue <= 0)
                {
                    Console.WriteLine("Invalid target value. Please enter a positive number.");
                    PressEnterToContinue();
                    return;
                }
                
                Console.Write("What is the bonus for completing the entire goal? ");
                if (!int.TryParse(Console.ReadLine(), out int bonusPoints) || bonusPoints < 0)
                {
                    Console.WriteLine("Invalid bonus value. Please enter a non-negative number.");
                    PressEnterToContinue();
                    return;
                }
                
                goal = new ProgressGoal(name, description, points, targetValue, bonusPoints);
                break;
                
            default:
                Console.WriteLine("Invalid goal type.");
                PressEnterToContinue();
                return;
        }
        
        questManager.AddGoal(goal);
        Console.WriteLine($"Goal '{name}' created successfully!");
        PressEnterToContinue();
    }
    
    // Save goals to a file
    static void SaveGoals(QuestManager questManager)
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        
        questManager.SaveToFile(filename);
        PressEnterToContinue();
    }
    
    // Load goals from a file
    static void LoadGoals(QuestManager questManager)
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        
        questManager.LoadFromFile(filename);
        PressEnterToContinue();
    }
    
    // Record an event for a goal
    static void RecordEvent(QuestManager questManager)
    {
        Console.Clear();
        questManager.ListGoals();
        
        if (Console.CursorTop == Console.WindowTop + 1)
        {
            // No goals yet
            PressEnterToContinue();
            return;
        }
        
        Console.Write("\nWhich goal did you accomplish? ");
        string input = Console.ReadLine();
        
        if (int.TryParse(input, out int goalIndex) && goalIndex > 0)
        {
            questManager.RecordEvent(goalIndex - 1);
        }
        else
        {
            Console.WriteLine("Invalid goal number. Please try again.");
        }
        
        PressEnterToContinue();
    }
    
    // Utility method to pause the program until the user presses Enter
    static void PressEnterToContinue()
    {
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }
}

/*
 * Creative additions:
 * 
 * 1. Leveling System: Added a leveling system where users earn titles as they gain points
 *    and progress through different levels, providing more motivation and recognition.
 * 
 * 2. Progress Goal Type: Added a new type of goal that tracks progress towards a larger goal,
 *    allowing users to record incremental progress and get points for each unit of progress.
 *    This is useful for goals like running a certain number of miles or reading a certain number of pages.
 * 
 * 3. Enhanced UI: Added a cleaner user interface with clear sections and visual separation,
 *    making the program more user-friendly.
 * 
 * 4. Progress Percentage Display: For Progress Goals, added a percentage display to help
 *    users visualize how close they are to completing their goals.
 */