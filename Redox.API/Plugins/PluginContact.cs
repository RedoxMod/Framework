using System;

namespace Redox.API.Plugins
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginContact : Attribute
    {
        public Uri Support { get; }
        
        public Uri Repository { get; }

        public PluginContact() {}
        
        public PluginContact(string support, string repository = "")
        {
            this.Support = new Uri(support);
            this.Repository = new Uri(repository);
        }
    }
}