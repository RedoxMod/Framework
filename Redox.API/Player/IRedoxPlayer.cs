﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Redox.API.Roles;
using Redox.API.Users;

namespace Redox.API.Player
{
    /// <summary>
    /// Representation of a player object.
    /// </summary>
    public interface IRedoxPlayer : IRedoxUser, IEquatable<IRedoxPlayer>
    {
        #region Properties
        
        string DisplayName { get; }
        
        string SteamID { get; }
        ulong USteamID { get; }
        float Ping { get; }
        
        float Health { get; set; }
        
        bool IsAdmin { get; }
        bool IsOnline { get; }
        bool IsAlive { get; }
        IPAddress Address { get; }
        
        CultureInfo Culture { get; }
        string Language { get; }
        
        Position Position { get; }
        
        IPlayerInventory Inventory { get; }
        
        IEnumerable<string> BlockedCommands { get; }
        #endregion

        #region Methods

        #region Player

        void SendMessage(string message);
        void SendMessage(string prefix, string message);
        
        void Teleport(float x, float y, float z);
        void Teleport(Position pos);
        void Kill();
        
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