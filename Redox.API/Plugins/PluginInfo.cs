using System;

namespace Redox.API.Plugins
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginInfo : Attribute
    {
        public string Title { get; private set; } = "Unknown";
        public string Description { get; private set; } = "This is a plugin";
        public string Authors { get; private set; } = "Unknown";
        public string Version { get; private set; } = "1.0.0";

        public PluginInfo() {}
        
        public PluginInfo(string title, string description, string authors, string version)
        {
            this.Title = title;
            this.Description = description;
            this.Authors = authors;
            this.Version = version;
        }
    }
}