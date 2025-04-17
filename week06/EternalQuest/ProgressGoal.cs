using System;

// Class for progress-based goals that track progress towards a larger goal
public class ProgressGoal : Goal
{
    private int _targetValue;
    private int _currentValue;
    private int _bonusPoints;
    
    // Constructor
    public ProgressGoal(string name, string description, int pointsPerUnit, int targetValue, int bonusPoints) 
        : base(name, description, pointsPerUnit)
    {
        _targetValue = targetValue;
        _currentValue = 0;
        _bonusPoints = bonusPoints;
    }
    
    // Constructor for loading from file
    public ProgressGoal(string name, string description, int pointsPerUnit, int targetValue, int bonusPoints, int currentValue) 
        : base(name, description, pointsPerUnit)
    {
        _targetValue = targetValue;
        _currentValue = currentValue;
        _bonusPoints = bonusPoints;
        _isComplete = currentValue >= targetValue;
    }
    
    // Record progress for this goal
    public override int RecordEvent()
    {
        if (_isComplete)
            return 0;
            
        Console.Write("Enter the amount of progress made: ");
        int progress = int.Parse(Console.ReadLine());
        
        if (progress <= 0)
        {
            Console.WriteLine("Progress must be greater than 0.");
            return 0;
        }
        
        int oldValue = _currentValue;
        _currentValue += progress;
        
        // If we just completed the goal, award bonus points
        if (oldValue < _targetValue && _currentValue >= _targetValue)
        {
            _isComplete = true;
            int pointsEarned = _points * progress + _bonusPoints;
            Console.WriteLine($"You completed the goal and earned a bonus of {_bonusPoints} points!");
            return pointsEarned;
        }
        else if (_currentValue > _targetValue)
        {
            // Only count progress up to the target
            int countedProgress = progress - (_currentValue - _targetValue);
            return _points * countedProgress;
        }
        
        return _points * progress;
    }
    
    // Check if the goal is complete
    public override bool IsComplete()
    {
        return _currentValue >= _targetValue;
    }
    
    // Get string representation for file storage
    public override string GetStringRepresentation()
    {
        return $"ProgressGoal:{_name},{_description},{_points},{_targetValue},{_bonusPoints},{_currentValue}";
    }
    
    // Get details string for display
    public override string GetDetailsString()
    {
        string completionStatus = _isComplete ? "[X]" : "[ ]";
        int progressPercentage = (_currentValue * 100) / _targetValue;
        return $"{completionStatus} {_name} ({_description}) -- Progress: {_currentValue}/{_targetValue} ({progressPercentage}%)";
    }
}