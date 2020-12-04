namespace Redox.Core.Events.Server
{
    public static class ServerEvents
    {
        public static event ServerInitializedDelegate U_OnServerInitialized;
        public static event ServerShutdownDelegate U_OnServerShutdown;
        public static event ServerSavedDelegate U_OnServerSaved;


        internal static void Init()
        {
            U_OnServerInitialized = delegate {  };
            U_OnServerShutdown = delegate {  };
            U_OnServerSaved = delegate {  };
        }

        public static void ServerInitialized()
        {
            U_OnServerInitialized?.Invoke();
        }

        public static void ServerShutdown()
        {
            U_OnServerShutdown?.Invoke();
        }

        public static void ServerSaved()
        {
            U_OnServerSaved?.Invoke();
        }
    }
}