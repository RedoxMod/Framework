using System;
using System.Collections.Generic;
using Redox.API.Plugins;

namespace Redox.Core.Plugins.CSharp
{
    public sealed class PluginAnalytics : IPluginAnalytics
    {

        /// <summary>
        /// List of Exceptions thrown in this plugin.
        /// </summary>
        public IList<Exception> Exceptions { get; }
        
        /// <summary>
        /// Returns the time in milliseconds of how long the plugin is been running.
        /// </summary>
        public double LifeTime { get; }
    }
}