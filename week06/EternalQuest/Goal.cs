using System;
using System.Collections.Generic;

// Base class for all types of goals
public abstract class Goal
{
    // Properties
    protected string _name;
    protected string _description;
    protected int _points;
    protected bool _isComplete;

    // Constructor
    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    // Methods
    public abstract int RecordEvent();  // Record that the goal was accomplished
    public abstract bool IsComplete();  // Check if the goal is complete
    public abstract string GetStringRepresentation();  // Get a string representation for saving to a file
    
    // Get formatted display string for showing in the list
    public abstract string GetDetailsString();
    
    // Getters
    public string GetName()
    {
        return _name;
    }
    
    public int GetPoints()
    {
        return _points;
    }
}