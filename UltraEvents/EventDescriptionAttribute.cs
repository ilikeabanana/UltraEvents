using System;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class EventDescriptionAttribute : Attribute
{
    public string Description { get; }
    public string Name { get; }
    public bool DefaultValue { get; }
    public bool requiresEnemies { get; }

    public EventDescriptionAttribute(string description, string name = null, bool defaultValue = true, bool requiresEnemies = false)
    {
        Description = description;
        Name = name;
        DefaultValue = defaultValue;
        this.requiresEnemies = requiresEnemies;
    }
}
