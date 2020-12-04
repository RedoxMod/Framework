using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Timers;

using Redox.Core.Components;

namespace Redox.Core.Timers
{
    [ComponentInfo("Timers", LoadPriority.Low)]
    public sealed class TimersProvider : ITimersProvider
    {
        private readonly IList<ITimer> _timers = new List<ITimer>();
        
        public ITimer Single(double interval, Action callBack)
        {
            ITimer timer = new Timer(TimerMode.Single, interval, callBack);    
            _timers.Add(timer);
            return timer;
        }

        public ITimer Infinite(double interval, Action callBack)
        {
            ITimer timer = new Timer(TimerMode.Infinite, interval, callBack, int.MaxValue);    
            _timers.Add(timer);
            return timer;
        }

        public ITimer Repeating(double interval, Action callBack, int amount)
        {
            ITimer timer = new Timer(TimerMode.Repeating, interval, callBack, amount);    
            _timers.Add(timer);
            return timer;
        }
        
        public Task RunAsync()
        {
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            return Task.CompletedTask;
        }
        
        public static ITimersProvider Get()
        {
            return ComponentsProvider.Get().ResolveComponent<ITimersProvider>();
        }
    }
}