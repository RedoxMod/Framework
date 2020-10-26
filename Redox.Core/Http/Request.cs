using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Redox.Core.Http
{
    public sealed class Request
    {

        public Uri Url { get; }
        
        public string Body { get; }
        
        public Action<int, string> CallBack { get; }
        
        public RequestMethod Method { get; }
        
        public string[] Headers { get; }

        public Task StartAsync()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(Url);
            request.Method = this.Method.ToString();
            request.Credentials = CredentialCache.DefaultCredentials;
            request.UserAgent = $"RedoxMod ({RedoxMod.GetMod().Version})";
            request.Timeout = 2000;
            if (Headers != null)
            {
                foreach(string header in Headers)
                    request.Headers.Add(header);
            }

            if (Method == RequestMethod.POST)
            {
                byte[] buffer = new byte[0];

                if (!string.IsNullOrEmpty(Body))
                {
                    buffer = Encoding.UTF8.GetBytes(Body);
                    request.ContentLength = buffer.Length;
                    request.ContentType = "application/x-www-form-urlencoded";
                }
            }

            return Task.CompletedTask;

        }
        
        
        public Request(Uri url, string body, Action<int, string> callBack, RequestMethod method = RequestMethod.GET, string[] headers = null)
        {
            Url = url;
            Body = body;
            CallBack = callBack;
            Method = method;
            Headers = headers;
        }
    }
}