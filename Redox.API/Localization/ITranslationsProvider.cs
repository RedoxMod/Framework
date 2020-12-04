using Redox.API.Components;
using Redox.API.Plugins;

namespace Redox.API.Localization
{
    public interface ITranslationsProvider : IBaseComponent
    {
        void Register(in ITranslationsRegistration registration);

        void Unregister(in IBasePlugin plugin);
        
    }
}