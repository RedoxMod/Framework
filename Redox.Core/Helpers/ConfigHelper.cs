using System.IO;
using Redox.API.Configuration;
using Redox.API.Plugins;
using Redox.Core.Serialization;

namespace Redox.Core.Helpers
{
    public static class ConfigHelper
    {
        public static void SaveOrLoadConfig(in IBasePlugin plugin, in IConfigurationContext context)
        {
            if (plugin == null || context == null) return;
            string fileName = Path.Combine(plugin.Directory.FullName, GetConfigExtension(context));

            if (!File.Exists(fileName))
            {
                context.Configuration.Load();
                SaveConfig(fileName, context);
            }
                
        }

        public static void SaveConfig(string filePath, in IConfigurationContext context)
        {
            switch (context.Info.Extension)
            {
               case ConfigExtension.Json:
                   JsonSerializer.ToFile(filePath, context.Configuration);
                   break;
               case ConfigExtension.Xml:
                   XmlSerializer.ToFile(filePath, context.Configuration);
                   break;
               case ConfigExtension.Yaml:
                   YamlSerializer.ToFile(filePath, context.Configuration);
                   break;
            }
        }

        public static string GetConfigExtension(in IConfigurationContext context)
        {
            if (context == null) return "Unknown";
            string name = context.Info.Name;
            switch (context.Info.Extension)
            {
                case ConfigExtension.Json:
                    name += ".json";
                    break;
                case ConfigExtension.Xml:
                    name += ".xml";
                    break;
                case ConfigExtension.Yaml:
                    name += ".yml";
                    break;
            }

            return name;
        }
    }
}