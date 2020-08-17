using System;

namespace Redox.API.Commands
{
    [Flags]
    public enum CommandCaller
    {
        Player, Console, Both
    }
}