using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redox.API.Components;
using Redox.API.Localization;
using Redox.API.Player;
using Redox.API.Plugins;

namespace Redox.Core.Localization
{
    [ComponentInfo("Translations", LoadPriority.Low)]
    public sealed class TranslationsProvider : ITranslationsProvider
    {
        
        private readonly IList<ITranslationsRegistration> _registrations = new List<ITranslationsRegistration>();
        
        public void Register(in ITranslationsRegistration registration)
        {
            if (registration == null) 
                return;
            
            string pluginTitle = registration.Plugin.Info.Title;

            if (_registrations.All(x => x.Plugin.Info.Title != pluginTitle))
                _registrations.Add(registration);
        }

        public void Unregister(in IBasePlugin plugin)
        {
            if (plugin == null)
                return;
            string pluginTitle = plugin.Info.Title;

            ITranslationsRegistration registration =
                _registrations.FirstOrDefault(x => x.Plugin.Info.Title == pluginTitle);
            if(registration != null)
                _registrations.Remove(registration);
        }
        
        public Task RunAsync()
        {
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public static TranslationsProvider Get()
        {
            return (TranslationsProvider)RedoxMod.GetMod().Components.ResolveComponent<ITranslationsProvider>();
        }
        
    }
}