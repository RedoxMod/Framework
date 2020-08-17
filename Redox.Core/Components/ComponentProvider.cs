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
        
        private readonly List<IComponentContext> _components = new List<IComponentContext>();
        
        /*
        public Task RegisterAssemblyAsync(Assembly assembly)
        {
            
        }
        */

        public Task<IBaseComponent> RegisterTypeAsync<TComponent>() where TComponent : IBaseComponent
        {
            Type type = typeof(TComponent);
            ComponentInfo info = type.GetCustomAttribute<ComponentInfo>();

            if (info == null)
            {
                Redox.GetMod().Logger.LogWarning("[Redox-Components] Failed to load component {0} because it's missing the \"ComponentInfo\" attribute!");
                return null;
            }

            IBaseComponent component = (IBaseComponent)Activator.CreateInstance(type);
            IComponentContext context = new ComponentContext(component, info);
            _components.Add(context);
            return Task.FromResult(component);
        }

        public Task<IBaseComponent> ResolveComponentAsync<TComponent>(string name = "") where TComponent : IBaseComponent
        {
            IBaseComponent component =
                (_components.FirstOrDefault(x => x.ComponentInfo.Name == name) ??
                 _components.FirstOrDefault(x => x.BaseComponent is TComponent)).BaseComponent;
            
            return component == null ? null : Task.FromResult(component);
        }

        public async Task StartAllAsync()
        {
            Console.WriteLine("[Redox] Loading components...");
            IOrderedEnumerable<IComponentContext> contexts =
                _components.OrderByDescending(x => (int)x.ComponentInfo.Priority);
            
            foreach (IComponentContext context in contexts)
            {
                await context.BaseComponent.RunAsync();
            }
        }
    }
}