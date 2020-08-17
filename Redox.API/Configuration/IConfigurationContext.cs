using System;
using Redox.API.Plugins;

namespace Redox.API.Configuration
{
    public interface IConfigurationContext
    {
        
        /// <summary>
        /// The configuration associated with the current context.
        /// </summary>
        IConfiguration Configuration { get; }
        
        /// <summary>
        /// The default configuration instance.
        /// </summary>
        object DefaultConfiguration { get; }
        
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