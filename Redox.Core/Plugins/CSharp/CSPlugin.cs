using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Redox.API.Commands;
using Redox.API.Configuration;
using Redox.API.Localization;
using Redox.API.Plugins;
using Redox.API.Roles;
using Redox.Core.Commands;
using Redox.Core.Configuration;
using Redox.Core.Localization;

namespace Redox.Core.Plugins.CSharp
{
    /// <summary>
    /// Generic presentation for a CSharp Plugin.
    /// </summary>
    public abstract class CSPlugin : IBasePlugin
    {

        private IDictionary<string, MethodInfo> _methods;
        
        public IPluginInfo Info { get; private set; }
        public IPluginSupport Support { get; set; }
        
        public IPluginAnalytics Analytics { get; set; }

        public PluginState State { get; internal set; } = PluginState.Loading;


        public IRolesProvider Roles => RedoxMod.GetMod().RolesProvider;
        
        public ITranslationsRegistration Translations { get; private set; }

        public IPluginManager PluginManager => RedoxMod.GetMod().PluginManager;

        public FileInfo FileInfo { get; internal set; }
        public DirectoryInfo Directory { get; private set; }
        
        protected abstract Task OnEnableAsync();
        protected abstract Task OnDisableAsync();
        
        protected virtual Task LoadTranslationsAsync()
        {
            return Task.CompletedTask;
        }
        public async Task LoadAsync() 
        {
            _methods = new Dictionary<string, MethodInfo>();
            
            Type type = this.GetType();
            
            this.Info = type.GetCustomAttribute<PluginInfoAttribute>() ?? new PluginInfoAttribute();
            this.Support = type.GetCustomAttribute<PluginSupportAttribute>() ?? new PluginSupportAttribute();
            
            this.Directory = new DirectoryInfo(Path.Combine(FileInfo.DirectoryName!, Info.Title));

            if (!Directory.Exists)
                Directory.Create();
            this.Translations = new TranslationsRegistration(this);

            if (!Translations.Exists)
            {
                await this.LoadTranslationsAsync();
                await Translations.SaveAsync();
            }
            else
            {
                await this.Translations.LoadAsync();
            }
            TranslationsProvider.Get().Register(this.Translations);
            await ConfigurationProvider.Get().RegisterAsync(this);
            await CommandsProvider.Get().RegisterAsync(this);
            //TODO: Insert command loader, config loader and translations loader.
            
            await this.LoadMethodsAsync();

            await this.OnEnableAsync();
            this.State = PluginState.Loaded;
        }

        public async Task UnloadAsync()
        {
            await this.OnDisableAsync();
            this.State = PluginState.Unloaded;
        }

        public Task LoadMethodsAsync()
        {
            Type type = this.GetType();
            
            foreach(MethodInfo method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                if(method.GetCustomAttribute<Collectable>() != null){
                    _methods.Add(method.Name, method);
                }
            }
            return Task.CompletedTask;
        }

        public async Task<object> CallAsync(string name, params object[] args)
        {
            return await Task.Run(() => this.Call(name, args));
        }

        public object Call(string name, params object[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                object ob = null;
                if(_methods.TryGetValue(name, out MethodInfo method))
                {
                    ob = method.Invoke(this, args);
                }
                return ob;
            }
            catch(Exception ex) 
            {
                RedoxMod.GetMod().TempLogger.Error("[CSharp-Error] Failed to invoke method {0} in {1} due to error: {2}", name, Info.Title, ex.Message);
                return null;
            }
            finally
            {
                sw.Stop();
                if(sw.ElapsedMilliseconds > 500)
                {
                    RedoxMod.GetMod().TempLogger.Info("[{0}] Invoking method {1} took more than 500 milliseconds. This is considered slow!");
                }
            }
        }

        public T Call<T>(string name, params object[] args)
        {
            return (T)this.Call(name, args);
        }

        protected async Task<T> GetConfigAsync<T>(string name) where T : IConfiguration
        {
            var config = await ConfigurationProvider.Get().ResolveAsync(this, name);
            return (T) config;
        }
    }
}