using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Redox.API.Roles;
using Redox.API.User;

namespace Redox.API.Player
{
    /// <summary>
    /// Representation of a player object.
    /// </summary>
    public interface IPlayer : IUser, IEquatable<IPlayer>
    {
        #region Information
        
        string DisplayName { get; }
        
        string SteamID { get; }
        ulong USteamID { get; }
        ushort Ping { get; }
        
        float Health { get; set; }
        
        bool IsAdmin { get; }
        bool IsOnline { get; }
        
        IPAddress Address { get; }
        
        object Object { get; }
        
        CultureInfo Culture { get; }
        string Language { get; }
        
        Position Position { get; }
        
        IEnumerable<string> BlockedCommands { get; }
        #endregion

        #region Methods

        #region Player

        void SendMessage(string message);
        void SendMessage(string prefix, string message);
        
        void Teleport(float x, float y, float z);
        void Teleport(Position pos);
        
        #endregion

        #region Permissions

        Task GivePermissionAsync(string perm);
        Task RemovePermissionAsync(string perm);

        Task<bool> HasPermissionAsync(string perm);
        #endregion

        #region Roles

        Task GiveRoleAsync(string rolename);
        Task GiveRoleAsync(IRole role);

        Task RemoveRoleAsync(string rolename);
        Task RemoveRoleAsync(IRole role);

        Task<bool> BelongsToRole(string rolename);
        
        #endregion
        
        #region Administration

        void Kick(string reason = "");
        void Ban(string reason = "");

        void BlockCommand(string cmd);
        void UnblockCommand(string cmd);
        #endregion

        #endregion
    }
}