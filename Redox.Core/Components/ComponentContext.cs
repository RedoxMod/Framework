using System;
using Redox.API.Components;

namespace Redox.Core.Components
{
    public sealed class ComponentContext : IComponentContext
    {
        /// <summary>
        /// The service of the component.
        /// </summary>
        public Type Service { get; }
        
        /// <summary>
        /// The implementation of the component service.
        /// </summary>
        public IBaseComponent BaseComponent { get; }
        
        /// <summary>
        /// Meta data about this component.
        /// </summary>
        public ComponentInfo ComponentInfo { get; }
        
        public ComponentContext(Type service, IBaseComponent baseComponent, ComponentInfo componentInfo)
        {
            Service = service;
            BaseComponent = baseComponent;
            ComponentInfo = componentInfo;
        }
    }
}