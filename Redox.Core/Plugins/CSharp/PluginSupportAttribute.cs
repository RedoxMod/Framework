using System;
using Redox.API.Plugins;

namespace Redox.Core.Plugins.CSharp
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginSupportAttribute : Attribute, IPluginSupport
    {
        public Uri Support { get; }
        
        public Uri Repository { get; }

        public PluginSupportAttribute() {}
        
        public PluginSupportAttribute(string support, string repository = "")
        {
            this.Support = new Uri(support);
            this.Repository = new Uri(repository);
        }
    }
}