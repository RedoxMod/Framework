using System;
using System.Collections.Generic;
using System.Diagnostics;

using Redox.API.Eventing;

namespace Redox.API.Plugins
{
    public sealed class PluginAnalytics
    {
        private readonly Stopwatch _stopwatch;
        
        /// <summary>
        /// List of Exceptions thrown in this plugin.
        /// </summary>
        public IList<Exception> Exceptions { get; }
        
        /// <summary>
        /// List of event listeners.
        /// </summary>
        public IList<IEventListener> Listeners { get; }

        /// <summary>
        /// Returns the time in milliseconds of how long the plugin is been running.
        /// </summary>
        public double LifeTime => _stopwatch.ElapsedMilliseconds;
    }
}