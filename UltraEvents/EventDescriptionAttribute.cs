using System;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class EventDescriptionAttribute : Attribute
{
    public string Description { get; }
    public string Name { get; }

    public EventDescriptionAttribute(string description, string name = null)
    {
        Description = description;
        Name = name;
    }
}
