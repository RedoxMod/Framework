using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Roles;
using Redox.Core.Components;
using Redox.Core.Serialization;


#pragma warning disable CS1998

namespace Redox.Core.Roles
{
    [ComponentInfo("RoleProvider", LoadPriority.Medium)]
    public sealed class RolesProvider : IRolesProvider
    {
        private IList<IRole> _roles = new List<IRole>();
        
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
                RedoxMod.GetMod().TempLogger.Info("[RedoxMod] Saving roles...");
                JsonSerializer.ToFile(_filePath, _roles);
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().TempLogger.Error("[RedoxMod] Failed to save roles data due to error: " + e.Message);
            }
            return Task.CompletedTask;

        }

        public Task LoadAsync()
        {
            try
            {
                RedoxMod.GetMod().TempLogger.Info("[RedoxMod] Loading roles...");
                if (!Exists)
                    _roles = new List<IRole>();
                else
                    _roles = JsonSerializer.FromFile<List<IRole>>(_filePath);
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().TempLogger.Error("[RedoxMod] Failed to load roles data due to error: " + e.Message);
                _roles = new List<IRole>();
            }

            return Task.CompletedTask;
        }

        public static RolesProvider Get()
        {
            return (RolesProvider)ComponentsProvider.Get().ResolveComponent<IRolesProvider>();
        }
    }
}