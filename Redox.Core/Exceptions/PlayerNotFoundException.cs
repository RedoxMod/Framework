using System;

namespace Redox.Core.Exceptions
{
    public class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException() {}

        public PlayerNotFoundException(string message) : base(message) { }
    }
}