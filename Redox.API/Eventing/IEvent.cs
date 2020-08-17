namespace Redox.API.Eventing
{
    public interface IEvent
    {
        /// <summary>
        /// The name of the event.
        /// </summary>
        string Name { get; }
    }
}