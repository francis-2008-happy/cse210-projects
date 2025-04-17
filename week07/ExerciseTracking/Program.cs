using System;
using System.Collections.Generic;

// Base Activity class
public abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    // Properties
    public DateTime Date { get { return _date; } }
    public int Minutes { get { return _minutes; } }

    // Abstract methods to be implemented by derived classes
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Virtual method that can be overridden if needed
    public virtual string GetSummary()
    {
        return $"{_date.ToString("dd MMM yyyy")} {GetType().Name} ({_minutes} min) - " +
               $"Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

// Running class
public class Running : Activity
{
    private double _distance; // in miles

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        // Speed = (distance / minutes) * 60
        return (_distance / Minutes) * 60;
    }

    public override double GetPace()
    {
        // Pace = minutes / distance
        return Minutes / _distance;
    }
}

// Cycling class
public class Cycling : Activity
{
    private double _speed; // in mph

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        // Distance = (speed * minutes) / 60
        return (_speed * Minutes) / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        // Pace = 60 / speed
        return 60 / _speed;
    }
}

// Swimming class
public class Swimming : Activity
{
    private int _laps;
    private const double LapDistanceInMiles = 50.0 / 1000 * 0.62;  // 50m in miles

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // Distance = laps * lap length in miles
        return _laps * LapDistanceInMiles;
    }

    public override double GetSpeed()
    {
        // Speed = (distance / minutes) * 60
        return (GetDistance() / Minutes) * 60;
    }

    public override double GetPace()
    {
        // Pace = minutes / distance
        return Minutes / GetDistance();
    }
}

// Program to test the classes
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Exercise Tracking Program\n");

        // Create activities
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 4), 45, 15.0),
            new Swimming(new DateTime(2022, 11, 5), 20, 20)
        };

        // Display summaries
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}