using System;
using Redox.API.Data;
using Redox.API.Plugins;

namespace Redox.API.Configuration
{
    public interface IConfigurationContext : ISaveable
    {
        
        /// <summary>
        /// The configuration associated with the current context.
        /// </summary>
        IConfiguration Configuration { get; }
        
        
        /// <summary>
        /// Meta data about this configuration.
        /// </summary>
        ConfigInfo Info { get; }
        
        /// <summary>
        /// The plugin associated with the current context.
        /// </summary>
        IBasePlugin Plugin { get; }
    }
}