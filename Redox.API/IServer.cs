﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Player;

namespace Redox.API
{
    public interface IServer : IBaseComponent, IPlayerManager
    {
        string ServerName { get; set; }
        
        string GameName { get; set; }
        
        int ServerPort { get; }
        
        int AppId { get;  }
        
        Version GameVersion { get; }
        
        CultureInfo Language { get; }

        void ReloadAsync();
        
        void ShutDownAsync();

        void SendMessage(string message);
        
        void SendMessage(string prefix, string message);

        void Ban(ulong id);

        void Unban(ulong id);
    }
}