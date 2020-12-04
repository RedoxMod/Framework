using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Data;
using Redox.Core.Components;
using Redox.Core.Events.Server;

namespace Redox.Core.Data
{
    [ComponentInfo("DataStore", LoadPriority.Medium)]
    public sealed class DataStore : IDataStore
    {

        private readonly object _obj = new object();
        
        private IDictionary<string, IDictionary<object, object>> _settings;
        
        private readonly string _filePath = Path.Combine(RedoxMod.GetMod().DataDirectory, "redox.dat");

        public bool Exists => File.Exists(_filePath);
        

        public async Task AppendAsync(string tableName, object key, object value)
        {
            if (!await this.HasTableAsync(tableName))
            {
                _settings.Add(tableName, new Dictionary<object, object>());
            }
            
            _settings[tableName].Add(key, value);
        }

        public async Task RemoveAsync(string tableName, object key)
        {
            if (await HasTableAsync(tableName))
            {
                _settings[tableName].Remove(key);
            }
        }

        public async Task RemoveTableAsync(string table)
        {
            if (await HasTableAsync(table))
            {
                _settings.Remove(table);
            }
        }

        public async Task<bool> HasAsync(string tableName, object key)
        {
            if (!await HasTableAsync(tableName)) return false;
            return _settings[tableName].ContainsKey(key);
        }

        public Task<bool> HasTableAsync(string tableName)
        {
            bool exists = _settings.ContainsKey(tableName);
            return Task.FromResult(exists);
        }

        public async Task<object> GetAsync(string tableName, object key)
        {
            if (!await HasTableAsync(tableName)) return null;
            _settings[tableName].TryGetValue(key, out object obj);
            return obj;
        }

        public async Task<IReadOnlyDictionary<object, object>> GetTableAsync(string tableName)
        {
            if (await HasTableAsync(tableName))
                return (IReadOnlyDictionary<object, object>)_settings[tableName];
            return null;
        }

        public async Task FlushAsync(string tableName)
        {
            if (await HasTableAsync(tableName))
                _settings[tableName].Clear();
        }

        public Task FlushAllAsync()
        {
            _settings.Clear();
            return Task.CompletedTask;
        }
        
        public Task SaveAsync()
        {
            lock (_obj)
            {
                try
                {
                    using (Stream stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, _settings);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return Task.CompletedTask;
        }

        public Task LoadAsync()
        {
            lock (_obj)
            {
                try
                {
                    using (Stream stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        _settings = (IDictionary<string, IDictionary<object, object>>)formatter.Deserialize(stream);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return Task.CompletedTask;
        }

        public async Task RunAsync()
        {
            await this.LoadAsync();
            ServerEvents.U_OnServerSaved += async() =>
            {
                await this.SaveAsync();
            };
        }

        public async Task ShutdownAsync()
        {
            await this.SaveAsync();
        }


        public static DataStore Get()
        {
            return (DataStore)ComponentsProvider.Get().ResolveComponent<IDataStore>();
        }
    }
}