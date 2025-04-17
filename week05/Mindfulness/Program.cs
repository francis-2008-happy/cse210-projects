using System;
using System.Collections.Generic;
using System.Threading;

// Exceeding Requirements
// To exceed requirements, I've implemented the following enhancements:

// 1. Activity Logging: The program now tracks and displays how many times each activity has been performed during the session.

// 2. Unique Prompt Selection: For the reflection and listing activities, prompts are selected randomly but won't repeat until all prompts have been used at least once in the session.

// 3. Enhanced Breathing Animation: The breathing activity now includes a more realistic animation that simulates the gradual inhale/exhale process.

// 4. Session Statistics: At the end of each session, the program displays statistics about activities completed.

class Program
{
    static Dictionary<string, int> activityCounts = new Dictionary<string, int>()
    {
        { "Breathing Activity", 0 },
        { "Reflection Activity", 0 },
        { "Listing Activity", 0 }
    };

    static int totalActivities = 0;

    static void Main(string[] args)
    {
        // Display menu and handle user input
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. View Session Statistics");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an activity: ");

            string choice = Console.ReadLine();

            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    activityCounts["Breathing Activity"]++;
                    totalActivities++;
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    activityCounts["Reflection Activity"]++;
                    totalActivities++;
                    break;
                case "3":
                    activity = new ListingActivity();
                    activityCounts["Listing Activity"]++;
                    totalActivities++;
                    break;
                case "4":
                    DisplaySessionStatistics();
                    continue;
                case "5":
                    DisplaySessionStatistics();
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(1000);
                    continue;
            }

            activity.Run();
        }
    }

    static void DisplaySessionStatistics()
    {
        Console.Clear();
        Console.WriteLine("Session Statistics");
        Console.WriteLine("=================");
        Console.WriteLine($"Total activities completed: {totalActivities}");
        foreach (var activity in activityCounts)
        {
            Console.WriteLine($"{activity.Key}: {activity.Value} times");
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}