namespace Redox.API.Components
{
    /// <summary>
    /// The load priority of defines how important a component is.
    /// <para>For example, The Logger Component is rather important because it's being used throughout Redox.</para>
    /// </summary>
    public enum LoadPriority
    {
        None, Low, Medium, High
    }
}