using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Roles;

using Redox.Core.Parsers;

namespace Redox.Core.Roles
{
    [ComponentInfo("PermissionsProvider", LoadPriority.Medium)]
    public sealed class PermissionsProvider : IPermissionsProvider
    {
        private IDictionary<ulong, HashSet<string>> _permissions;

        private readonly string _filePath = Path.Combine(RedoxMod.GetMod().DataDirectory, "redox.permissions.json");

        public bool Exists => File.Exists(_filePath);
        
        public Task GiveAsync(ulong playerId, string permission)
        {
            if(!_permissions.ContainsKey(playerId))
                _permissions.Add(playerId, new HashSet<string>());
            _permissions[playerId].Add(permission);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(ulong playerId, string permission)
        {
            if (_permissions.ContainsKey(playerId))
                _permissions[playerId].Remove(permission);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> GetAsync(ulong playerId)
        {
            return _permissions.ContainsKey(playerId) ? Task.FromResult(_permissions[playerId].AsEnumerable()) : null;
        }
        
        public async Task RunAsync()
        {
            await this.LoadAsync();
        }

        public async Task ShutdownAsync()
        {
            await this.SaveAsync();
        }

        
        public Task SaveAsync()
        {
            try
            {
                RedoxMod.GetMod().Logger.Info("[RedoxMod] Saving permissions...");
                JsonParser.ToFile(_filePath, _permissions);
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().Logger.Error("[RedoxMod] Failed to save permissions data due to error: " + e.Message);
            }
            return Task.CompletedTask;

        }

        public Task LoadAsync()
        {
            try
            {
                RedoxMod.GetMod().Logger.Info("[RedoxMod] Loading permissions...");
                if(!Exists)
                    _permissions = new Dictionary<ulong, HashSet<string>>();
                else
                    _permissions = JsonParser.FromFile<IDictionary<ulong, HashSet<string>>>(_filePath);
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().Logger.Error("[RedoxMod] Failed to load permissions data due to error: " + e.Message);
                _permissions = new Dictionary<ulong, HashSet<string>>();
            }

            return Task.CompletedTask;
        }
    }
}