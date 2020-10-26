using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

using Redox.API.Database;

namespace Redox.Core.Database
{
    public sealed class MySqlDatabase : IConnection
    {
        public string Server { get; }
        
        public string Username { get; }
        
        public string Password { get; }
        
        public string Database { get; }
        
        public uint Port { get; }

        public DbConnection DbConnection => Connection;
        
        public MySqlConnection Connection { get; }
        
        public bool IsOpen => Connection?.State == ConnectionState.Open;
        
        
        
        public async Task<int> QueryAsync(string statement)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand(statement, Connection))
                {
                    return await command.ExecuteNonQueryAsync();
                }
                
            }
            catch (MySqlException ex)
            {
                RedoxMod.GetMod().Logger.Error("[Redox.MySQL] Error: " + ex.Message);
                return 0;
            }
        }

        public async Task<List<Dictionary<string, object>>> GetResultsAsync(string statement)
        {
            try
            {
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

                using (MySqlCommand command = new MySqlCommand(statement, Connection))
                {
                    DbDataReader reader = await command.ExecuteReaderAsync();
            
                    while (await reader.ReadAsync())
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string column = reader.GetName(i);
                            object value = reader.GetValue(i);
                            row.Add(column, value);
                        }
                        results.Add(row);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                RedoxMod.GetMod().Logger.Error("[Redox.MySQL] Error: " + ex.Message);
                return null;
            }
           
        }

        public async Task<List<T>> GetResultsAsync<T>(string statement) where T : class
        {
            try
            {
                const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                List<T> list = new List<T>();
                Type type = typeof(T);
                if (type.GetProperties(flags).Length == 0)
                {
                    RedoxMod.GetMod().Logger.Warning("[Redox.MySQL] Failed to execute reader because {0} has no public properties!", nameof(T));
                    return null;
                }
                
                var results = await GetResultsAsync(statement);
                foreach (var dict in results)
                {
                    T instance = Activator.CreateInstance<T>();
                    
                    foreach (var pair in dict)
                    {
                        string name = pair.Key;
                        object value = pair.Value;

                        PropertyInfo prop = type.GetProperty(name);
                        if (prop == null) continue;
                        if(!prop.CanWrite) continue;
                        if (prop.PropertyType == value.GetType())
                        {
                            prop.SetValue(instance, value);
                        }
                    }
                    list.Add(instance);
                }

                return list;
            }
            catch (MySqlException ex)
            {
                RedoxMod.GetMod().Logger.Error("[Redox.MySQL] Error: " + ex.Message);
                return null;
            }
        }
        
        public async Task<bool> OpenAsync()
        {
            try
            {
                if (IsOpen) return true;
                await Connection.OpenAsync();
                return true;
            }
            catch (MySqlException ex)
            {
                RedoxMod.GetMod().Logger.Error("[Redox.MySQL] Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> CloseAsync()
        {
            try
            {
                if (!IsOpen) return true;
                await Connection.CloseAsync();
                return true;

            }
            catch (MySqlException ex)
            {
                RedoxMod.GetMod().Logger.Error("[Redox.MySQL] Error: " + ex.Message);
                return false;
            }
        }
        
        public MySqlDatabase(string server, string username, string password, string database, uint port = 3306)
        {
            Server = server;
            Username = username;
            Password = password;
            Database = database;
            Port = port;

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder()
            {
                Server = server,
                UserID = username,
                Password = password,
                Database = database,
                Port = port
            };
            Connection = new MySqlConnection(builder.ToString());
        }

        public static MySqlDatabase Create(string server, string username, string password, string database, uint port = 3306)
        {
            return new MySqlDatabase(server, username, password, database, port);
        }
    }
}