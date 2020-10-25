using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Redox.API;
using Redox.API.Commands;
using Redox.API.Components;
using Redox.API.Configuration;
using Redox.API.Database;
using Redox.API.Engines;
using Redox.API.Plugins;
using Redox.API.Roles;

using Redox.Core.Commands;
using Redox.Core.Components;
using Redox.Core.Configuration;
using Redox.Core.Configuration.Redox;
using Redox.Core.Database;
using Redox.Core.Engines;
using Redox.Core.Http;
using Redox.Core.Parsers;
using Redox.Core.Plugins;
using Redox.Core.Roles;

namespace Redox.Core
{
    public sealed class RedoxMod
    {
        private static RedoxMod _instance;

        public static RedoxMod GetMod()
        {
            return _instance ??= new RedoxMod();
        }
        
        public readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        
        
        #region Properties
        
        public RedoxConfig Config { get; } = new RedoxConfig();
        
        #region Directories

        public string RootDirectory { get; } = "RedoxMod\\";
        public string PluginDirectory { get; private set; }
        public string DataDirectory { get; private set; }
        public string LoggingDirectory { get; private set; }

        public string DependenciesDirectory {get; private set;}
        #endregion

        #region Components

        public IComponentProvider Components => ComponentProvider.Get();

        public ILogger Logger => Components.ResolveComponent<ILogger>();

        public IRolesProvider RolesProvider => Components.ResolveComponent<IRolesProvider>();
        
        public IPluginEngineProvider PluginEngineProvider => Components.ResolveComponent<IPluginEngineProvider>();

        public IPluginManager PluginManager => Components.ResolveComponent<IPluginManager>();
        
        #endregion
        
        #endregion
        
        public async Task InitializeAsync()
        {
            PluginDirectory = "Plugins";
            DataDirectory = "Data";
            LoggingDirectory = "Logs";
            DependenciesDirectory = "Dependencies";
            if (!Directory.Exists(RootDirectory)) Directory.CreateDirectory(RootDirectory);
            Directory.SetCurrentDirectory(RootDirectory);
            if (!Directory.Exists(PluginDirectory)) Directory.CreateDirectory(PluginDirectory);
            if (!Directory.Exists(DataDirectory)) Directory.CreateDirectory(DataDirectory);
            if (!Directory.Exists(LoggingDirectory)) Directory.CreateDirectory(LoggingDirectory);
            if (!Directory.Exists(DependenciesDirectory)) Directory.CreateDirectory(DependenciesDirectory);
            
            //Used for local testing. Will be removed in the future.
            File.WriteAllText("redox.config.xml", XmlParser.ToXml(Config.Init()));
            this.RegisterComponents();
        }

        private async void RegisterComponents()
        {
            Components.RegisterType<ILogger, Logger>();
            Components.RegisterType<IRolesProvider, RolesProvider>();
            Components.RegisterType<IPermissionsProvider, PermissionsProvider>();
            Components.RegisterType<IConfigurationProvider, ConfigurationProvider>();
            Components.RegisterType<IPluginEngineProvider, PluginEngineProvider>();
            Components.RegisterType<IPluginManager, PluginManager>();
            Components.RegisterType<ICommandProvider, CommandProvider>();

            await Components.StartAllAsync();
        }

        public async Task ShutdownAsync()
        {
            
        }
    }
}