using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Roles;

namespace Redox.Core.Roles
{
    [ComponentInfo("PermissionsProvider", LoadPriority.Medium)]
    public sealed class PermissionsProvider : IPermissionsProvider
    {
        private IDictionary<ulong, HashSet<string>> _permissions;

        private readonly string _filePath = Path.Combine(RedoxMod.GetMod().DataDirectory, "redox.permissions.dat");
        
        public Task GivePermissionToPlayerAsync(ulong playerid, string permission)
        {
            if(!_permissions.ContainsKey(playerid))
                _permissions.Add(playerid, new HashSet<string>());
            _permissions[playerid].Add(permission);
            return Task.CompletedTask;
        }

        public Task RemovePermissionFromPlayerAsync(ulong playerid, string permission)
        {
            if (_permissions.ContainsKey(playerid))
                _permissions[playerid].Remove(permission);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> GetPlayerPermissionsAsync(ulong playerid)
        {
            return _permissions.ContainsKey(playerid) ? Task.FromResult(_permissions[playerid].AsEnumerable()) : null;
        }
        
        public Task RunAsync()
        {
            try
            {
                RedoxMod.GetMod().Logger.LogInfo("[RedoxMod] Loading permissions...");
                if(!File.Exists(_filePath))
                    _permissions = new Dictionary<ulong, HashSet<string>>();
                else
                {
                    using (FileStream stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        _permissions = (Dictionary<ulong, HashSet<string>>)formatter.Deserialize(stream);
                    }
                }
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().Logger.LogError("[RedoxMod] Failed to load permissions data due to error: " + e.Message);
                _permissions = new Dictionary<ulong, HashSet<string>>();
            }
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            try
            {
                using (FileStream stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, _permissions);
                }
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().Logger.LogError("[RedoxMod] Failed to save permissions data due to error: " + e.Message);
            }
            return Task.CompletedTask;
        }
    }
}