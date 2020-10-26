using System.Collections.Generic;
using System.Threading.Tasks;

using Redox.API.Components;

namespace Redox.Core.Http
{
    [ComponentInfo("WebRequestManager", LoadPriority.Low)]
    public sealed class WebRequestManager : IBaseComponent
    {
        private static WebRequestManager _instance;
        
        public readonly Queue<Request> RequestsQueue = new Queue<Request>();
        public readonly List<Request> Requests = new List<Request>();

        public static WebRequestManager Get()
        {
            return _instance ??= new WebRequestManager();
        }
        public async Task RunAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task ShutdownAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}