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
using Redox.API.Localization;
using Redox.API.Logging;
using Redox.API.Plugins;
using Redox.API.Roles;
using Redox.API.Timers;
using Redox.Core.Commands;
using Redox.Core.Components;
using Redox.Core.Configuration;
using Redox.Core.Configuration.Redox;
using Redox.Core.Database;
using Redox.Core.Engines;
using Redox.Core.Events;
using Redox.Core.Http;
using Redox.Core.Localization;
using Redox.Core.Logging;
using Redox.Core.Plugins;
using Redox.Core.Plugins.CSharp;
using Redox.Core.Roles;
using Redox.Core.Timers;

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

        public string RootDirectory { get; private set; } = Path.Combine(Directory.GetCurrentDirectory(), "RedoxMod");
        public string PluginDirectory => Path.Combine(RootDirectory, "Plugins");
        public string DataDirectory => Path.Combine(RootDirectory, "Data");
        public string LoggingDirectory => Path.Combine(RootDirectory, "Logs");
        public string DependenciesDirectory => Path.Combine(RootDirectory, "Dependencies");
        public string ExtensionsDirectory => Path.Combine(RootDirectory, "Extensions");
        
        #endregion

        #region Components

        public IComponentsProvider Components => ComponentsProvider.Get();

        public TempLogger TempLogger => TempLogger.Get();

        private ILogger _logger;
        public ILogger Logger
        {
            get => _logger;
            set => _logger ??= value;
        }

        private IServer _server;

        public IServer Server
        {
            get => _server;
            set => _server ??= value;
        }
        
        public IRolesProvider RolesProvider => Components.ResolveComponent<IRolesProvider>();
        
        public IPluginEngineProvider PluginEngineProvider => Components.ResolveComponent<IPluginEngineProvider>();

        public IPluginManager PluginManager => Components.ResolveComponent<IPluginManager>();
        
        #endregion
        
        #endregion

        public async Task InitializeAsync(string modDir)
        {
            if (!string.IsNullOrEmpty(modDir))
                RootDirectory = modDir;
            
            if (!Directory.Exists(RootDirectory)) Directory.CreateDirectory(RootDirectory);
            if (!Directory.Exists(PluginDirectory)) Directory.CreateDirectory(PluginDirectory);
            if (!Directory.Exists(DataDirectory)) Directory.CreateDirectory(DataDirectory);
            if (!Directory.Exists(LoggingDirectory)) Directory.CreateDirectory(LoggingDirectory);
            if (!Directory.Exists(DependenciesDirectory)) Directory.CreateDirectory(DependenciesDirectory);
            if (!Directory.Exists(ExtensionsDirectory)) Directory.CreateDirectory(ExtensionsDirectory);
            
            Hooks.InitHooks();
            this.RegisterComponents();
            
            await PluginEngineProvider.RegisterAsync<ExtensionsEngine>();
            await PluginEngineProvider.RegisterAsync<CSharpEngine>();
            await PluginEngineProvider.StartAllAsync();
        }
        private async void RegisterComponents()
        {
           // Components.RegisterType<TempLogger, TempLogger>();
            Components.RegisterType<IRolesProvider, RolesProvider>();
            Components.RegisterType<IPermissionsProvider, PermissionsProvider>();
            Components.RegisterType<IConfigurationProvider, ConfigurationProvider>();
            Components.RegisterType<IPluginEngineProvider, PluginEngineProvider>();
            Components.RegisterType<IPluginManager, PluginManager>();
            Components.RegisterType<ICommandsProvider, CommandsProvider>();
            Components.RegisterType<ITranslationsProvider, TranslationsProvider>();
            Components.RegisterType<ITimersProvider, TimersProvider>();
            await Components.StartAllAsync();
        }

        public async Task ShutdownAsync()
        {
            
        }
    }
}