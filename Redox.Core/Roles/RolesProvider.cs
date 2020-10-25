using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Roles;
using Redox.Core.Parsers;


#pragma warning disable CS1998

namespace Redox.Core.Roles
{
    [ComponentInfo("RoleProvider", LoadPriority.Medium)]
    public sealed class RolesProvider : IRolesProvider
    {
        private IList<IRole> _roles;
        
        private readonly string _filePath = Path.Combine(RedoxMod.GetMod().DataDirectory, "redox.roles.json");

        public bool Exists => File.Exists(_filePath);
        
        public async Task AddAsync(IRole role)
        {
            bool exists = await HasAsync(role.Name);
            if (!exists)
                _roles.Add(role);
        }

        public async Task RemoveAsync(string name)
        {
            bool exists = await HasAsync(name);
            if (exists)
            {
                IRole role = await GetAsync(name);
                _roles.Remove(role);
            }
        }
        public Task<IRole> GetAsync(string name)
        {
            return Task.FromResult(_roles.FirstOrDefault(x => x.Name == name));
        }

        public Task<bool> HasAsync(string name)
        {
            return Task.FromResult(_roles.Any(x => x.Name == name));
        }
        public async Task RunAsync()
        {
            await LoadAsync();
        }

        public async Task ShutdownAsync()
        {
            await SaveAsync();
        }
        
        public Task SaveAsync()
        {
            try
            {
                RedoxMod.GetMod().Logger.Info("[RedoxMod] Saving roles...");
                JsonParser.ToFile(_filePath, _roles);
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().Logger.Error("[RedoxMod] Failed to save roles data due to error: " + e.Message);
            }
            return Task.CompletedTask;

        }

        public Task LoadAsync()
        {
            try
            {
                RedoxMod.GetMod().Logger.Info("[RedoxMod] Loading roles...");
                if (!Exists)
                    _roles = new List<IRole>();
                else
                    _roles = JsonParser.FromFile<List<IRole>>(_filePath);
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().Logger.Error("[RedoxMod] Failed to load roles data due to error: " + e.Message);
                _roles = new List<IRole>();
            }

            return Task.CompletedTask;
        }
    }
}