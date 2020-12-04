using System;
using Redox.API.Components;

namespace Redox.API.Timers
{
    public interface ITimersProvider : IBaseComponent
    {
        ITimer Single(double interval, Action callBack);

        ITimer Infinite(double interval, Action callBack);
        
        ITimer Repeating(double interval, Action callBack, int amount);
    }
}