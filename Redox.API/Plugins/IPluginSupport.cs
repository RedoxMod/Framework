using System;

namespace Redox.API.Plugins
{
    public interface IPluginSupport
    {
        Uri Support { get; }
        
        Uri Repository { get; }
    }
}