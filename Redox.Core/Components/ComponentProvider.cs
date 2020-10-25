using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Redox.API.Components;

namespace Redox.Core.Components
{
    public sealed class ComponentProvider : IComponentProvider
    {

        private static ComponentProvider _instance;
        
        
        private readonly List<IComponentContext> _components = new List<IComponentContext>();

        public static ComponentProvider Get()
        {
            return _instance ??= new ComponentProvider();
        }
        public void RegisterType<TService, TImplementation>() where TImplementation : IBaseComponent
        {
            Type type = typeof(TImplementation);
            ComponentInfo info = type.GetCustomAttribute<ComponentInfo>();

            if (info == null)
            {
                RedoxMod.GetMod().Logger.Warning("[RedoxMod-Components] Failed to load component {0} because it's missing the \"ComponentInfo\" attribute!");
                return;
            }

            IBaseComponent component = (IBaseComponent)Activator.CreateInstance(type);
            IComponentContext context = new ComponentContext(typeof(TService), component, info);
            _components.Add(context);
        }

        /*
        public void RegisterType<TService, TImplementation>() where TImplementation : IBaseComponent;
        {
          
        }
       */
        public TService ResolveComponent<TService>()
        {
            IBaseComponent component =
                (_components.FirstOrDefault(x => x.BaseComponent is TService))?.BaseComponent;
            
            return (TService)component;
        }

        public async Task StartAllAsync()
        {
            RedoxMod.GetMod().Logger.Info("[RedoxMod] Loading components...");
            IEnumerable<IComponentContext> components =
                (from x in _components where x.ComponentInfo.Priority != LoadPriority.None select x);
            IOrderedEnumerable<IComponentContext> contexts =
                components.OrderByDescending(x => (int)x.ComponentInfo.Priority);
            
            foreach (IComponentContext context in contexts)
            {
                try
                {
                    await context.BaseComponent.RunAsync();
                    RedoxMod.GetMod().Logger.Info("[Component] Loading component {0}", context.ComponentInfo.Name);
                }
                catch (Exception e)
                {
                    RedoxMod.GetMod().Logger.Error("[Components] Failed to load component {0} due to error: {1}", context.ComponentInfo.Name, e.Message);
                }
                
            }
        }
    }
}