namespace Redox.API.Plugins
{
    public interface IPluginInfo
    {
        string Title { get; }
        
        string Description { get; }
        
        string Authors { get; }
        
        string Version { get; }
    }
}