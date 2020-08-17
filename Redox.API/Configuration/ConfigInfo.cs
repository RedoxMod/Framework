using System;

namespace Redox.API.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigInfo : Attribute
    {
        /// <summary>
        /// The name of the configuration file.
        /// <para>Example: BanConfiguration.</para> <
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// The type of configuration.
        /// </summary>
        public ConfigExtension Extension { get; }
        

        public ConfigInfo(string name, ConfigExtension extension)
        {
            this.Name = name;
            this.Extension = extension;
        }
    }
}