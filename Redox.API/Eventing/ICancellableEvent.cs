namespace Redox.API.Eventing
{
    public interface ICancellableEvent
    {
        /// <summary>
        /// Cancels the event.
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        bool Cancel(bool cancel);
    }
}