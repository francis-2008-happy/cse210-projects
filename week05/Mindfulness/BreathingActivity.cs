using System;
using System.Threading;

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing Activity";
        _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    protected override void PerformActivity()
    {
        int cycles = _duration / 6; // Each breath cycle takes about 6 seconds
        for (int i = 0; i < cycles; i++)
        {
            // Enhanced breathing animation
            Console.WriteLine("Breathe in...");
            for (int j = 1; j <= 3; j++)
            {
                Console.Write(j + "... ");
                Thread.Sleep(1000);
            }
            Console.WriteLine("\nHold...");
            Thread.Sleep(1000);
            
            Console.WriteLine("Breathe out...");
            for (int j = 1; j <= 3; j++)
            {
                Console.Write(j + "... ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }
}