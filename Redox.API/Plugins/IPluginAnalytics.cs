using System;
using System.Collections.Generic;

namespace Redox.API.Plugins
{
    public interface IPluginAnalytics
    {
        IList<Exception> Exceptions { get; }
        
        double LifeTime { get; }
    }
}