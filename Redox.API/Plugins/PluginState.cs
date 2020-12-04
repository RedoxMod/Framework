namespace Redox.API.Plugins
{
    /// <summary>
    /// Represents the state of a plugin.
    /// </summary>
    public enum PluginState
    {
        Loading, Loaded, Unloaded, Unloading, Failed
    }
}