using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using Redox.API.Components;

namespace Redox.Core.Http
{
    public sealed class WebRequestProvider : IBaseComponent
    {
        
        public void Create(string url, string body, Action<int, string> callBack, RequestMethod method = RequestMethod.GET, string[] headers = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri uri))
            {
                Request request = new Request(uri, body, callBack, method, headers);
                WebRequestManager.Get().RequestsQueue.Enqueue(request);
            }
            else
            {
                RedoxMod.GetMod().TempLogger.Warning("[RedoxMod] Failed to create WebRequest due to invalid URl");
            }
        }
        
        public Task RunAsync()
        {
            RedoxMod.GetMod().TempLogger.Info("[RedoxMod] Loading WebRequestProvider...");
            
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.DefaultConnectionLimit = 200;
            
            return Task.CompletedTask;
        }

        private bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
        {
            return true;
        }

        public Task ShutdownAsync()
        {
            return Task.CompletedTask;
        }
    }
}