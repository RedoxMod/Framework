using System;
using System.Reflection;

namespace Redox.API.Components
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentInfo : Attribute
    {
        public string Name { get; }

        public LoadPriority Priority { get; private set; } = LoadPriority.Low;

        public ComponentInfo(string name)
        {
            this.Name = name;
        }

        public ComponentInfo(string name, LoadPriority priority)
        {
            this.Name = name;
            this.Priority = priority;
        }
    }
}