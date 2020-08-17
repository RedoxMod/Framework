using System.IO;
using System.Threading.Tasks;
using Redox.API;
using Redox.API.Components;
using Redox.API.Roles;

namespace Redox.Core
{
    public sealed class Redox
    {
        private static Redox _instance;

        public static Redox GetMod()
        {
            return _instance ??= new Redox();
        }

        #region Properties
        
        #region Directories

        public string RootDirectory { get; private set; } = "Redox\\";
        public string PluginDirectory { get; private set; }
        public string DataDirectory { get; private set; }
        public string LoggingDirectory { get; private set; }
        #endregion

        #region Components
        
        public IComponentProvider ComponentProvider { get; private set; }
        
        public ILogger Logger { get; private set; }
        
        public IRoleFactory RoleFactory { get; private set; }
        public IRoleProvider RoleProvider { get; private set; }
        
        #endregion
        
        
        
        #endregion
        public async Task InitializeAsync()
        {
            PluginDirectory = Path.Combine(RootDirectory, "Plugins\\");
            DataDirectory = Path.Combine(RootDirectory, "Data\\");
            LoggingDirectory = Path.Combine(RootDirectory, "Logs\\");
            
            
            if (!Directory.Exists(RootDirectory)) Directory.CreateDirectory(RootDirectory);
            Directory.SetCurrentDirectory(RootDirectory);
            if (!Directory.Exists(PluginDirectory)) Directory.CreateDirectory(PluginDirectory);
            if (!Directory.Exists(DataDirectory)) Directory.CreateDirectory(DataDirectory);
            if (!Directory.Exists(LoggingDirectory)) Directory.CreateDirectory(LoggingDirectory);
            
            
        }
    }
}