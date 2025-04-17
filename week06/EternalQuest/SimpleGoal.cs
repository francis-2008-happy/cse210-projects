using System;

// Class for simple one-time goals
public class SimpleGoal : Goal
{
    // Constructor
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points)
    {
    }
    
    // Constructor for loading from file
    public SimpleGoal(string name, string description, int points, bool isComplete) 
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }
    
    // Record an event for this goal
    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }
    
    // Check if the goal is complete
    public override bool IsComplete()
    {
        return _isComplete;
    }
    
    // Get string representation for file storage
    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{_name},{_description},{_points},{_isComplete}";
    }
    
    // Get details string for display
    public override string GetDetailsString()
    {
        string completionStatus = _isComplete ? "[X]" : "[ ]";
        return $"{completionStatus} {_name} ({_description})";
    }
}