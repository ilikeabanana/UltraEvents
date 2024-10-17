using System;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class EventDescriptionAttribute : Attribute
{
    public string Description { get; }
    public string Name { get; }
    public bool DefaultValue { get; }

    public EventDescriptionAttribute(string description, string name = null, bool defaultValue = true)
    {
        Description = description;
        Name = name;
        DefaultValue = defaultValue;
    }
}
