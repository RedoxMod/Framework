namespace Redox.API.User
{
    /// <summary>
    /// Represents a generic user in Redox
    /// </summary>
    public interface IUser
    {
        string Name { get; }

        bool IsPlayer { get; }
        bool IsConsole { get; }

        void Reply(string message);
        
        void Reply(string prefix, string message);
    }
}