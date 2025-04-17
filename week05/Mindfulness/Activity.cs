using System;
using System.Threading;

abstract class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public void Run()
    {
        DisplayStartingMessage();
        PrepareToBegin();
        PerformActivity();
        DisplayEndingMessage();
    }

    protected void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Starting {_name}");
        Console.WriteLine(_description);
        Console.Write("Enter duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());
    }

    protected void PrepareToBegin()
    {
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine("Good job!");
        ShowSpinner(2);
        Console.WriteLine($"You have completed the {_name} for {_duration} seconds.");
        ShowSpinner(3);
    }

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write("\b\b\b   \b\b\b");
        }
        Console.WriteLine();
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
            Console.Write("\b\b \b");
        }
        Console.WriteLine();
    }

    protected abstract void PerformActivity();
}