using System;

namespace Redox.Core.Plugins.CSharp
{
    /// <summary>
    /// Any method carrying this attribute will be collected by the hook collector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class Collectable : Attribute {}
}