using Redox.API.Configuration;

namespace Redox.Core.Configuration
{
    public abstract class Configuration : IConfiguration
    {
        protected abstract void LoadDefaults();
        
        public void Load()
        {
            this.LoadDefaults();
        }
    }
}