namespace Redox.API.Components
{
    public interface IComponentContext
    {
        /// <summary>
        /// The component associated with this context.
        /// </summary>
        IBaseComponent BaseComponent { get; }
        
        /// <summary>
        /// Information about this component.
        /// </summary>
        ComponentInfo ComponentInfo { get; }
        
        
    }
}