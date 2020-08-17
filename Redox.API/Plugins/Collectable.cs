using System;

namespace Redox.API.Plugins
{
    /// <summary>
    /// Any method carrying this attribute will be collected by the hook collector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class Collectable : Attribute {}
}