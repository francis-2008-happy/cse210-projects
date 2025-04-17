using System;

// Class for checklist goals that must be completed a certain number of times
public class ChecklistGoal : Goal
{
    private int _target;
    private int _completedCount;
    private int _bonus;
    
    // Constructor
    public ChecklistGoal(string name, string description, int points, int target, int bonus) 
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _completedCount = 0;
    }
    
    // Constructor for loading from file
    public ChecklistGoal(string name, string description, int points, int target, int bonus, int completedCount) 
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _completedCount = completedCount;
        _isComplete = completedCount >= target;
    }
    
    // Record an event for this goal
    public override int RecordEvent()
    {
        if (_completedCount < _target)
        {
            _completedCount++;
            
            if (_completedCount >= _target)
            {
                _isComplete = true;
                return _points + _bonus; // Award bonus points when target is reached
            }
            return _points;
        }
        return 0;
    }
    
    // Check if the goal is complete
    public override bool IsComplete()
    {
        return _completedCount >= _target;
    }
    
    // Get string representation for file storage
    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_name},{_description},{_points},{_target},{_bonus},{_completedCount}";
    }
    
    // Get details string for display
    public override string GetDetailsString()
    {
        string completionStatus = _isComplete ? "[X]" : "[ ]";
        return $"{completionStatus} {_name} ({_description}) -- Currently completed {_completedCount}/{_target}";
    }
}