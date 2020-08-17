using Redox.API.Components;

namespace Redox.Core.Components
{
    public sealed class ComponentContext : IComponentContext
    {
        public IBaseComponent BaseComponent { get; }
        public ComponentInfo ComponentInfo { get; }
        
        public ComponentContext(IBaseComponent baseComponent, ComponentInfo componentInfo)
        {
            BaseComponent = baseComponent;
            ComponentInfo = componentInfo;
        }
    }
}