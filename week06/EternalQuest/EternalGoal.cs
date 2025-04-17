using System;

// Class for eternal goals that are never complete
public class EternalGoal : Goal
{
    // Constructor
    public EternalGoal(string name, string description, int points) 
        : base(name, description, points)
    {
    }
    
    // Record an event for this goal
    public override int RecordEvent()
    {
        return _points;
    }
    
    // Eternal goals are never complete
    public override bool IsComplete()
    {
        return false;
    }
    
    // Get string representation for file storage
    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{_name},{_description},{_points}";
    }
    
    // Get details string for display
    public override string GetDetailsString()
    {
        return $"[ ] {_name} ({_description})";
    }
}