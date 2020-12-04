using System;
using Redox.API.Plugins;

namespace Redox.Core.Plugins.CSharp
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginInfoAttribute : Attribute, IPluginInfo
    {
        public string Title { get; private set; } = "Unknown";

        public string Description { get; private set; } = "This is a plugin";

        public string Authors { get; private set; } = "Unknown";
        
        public string Version { get; private set; } = "1.0.0";

        public PluginInfoAttribute() {}
        
        public PluginInfoAttribute(string title, string description, string authors, string version)
        {
            this.Title = title;
            this.Description = description;
            this.Authors = authors;
            this.Version = version;
        }
    }
}