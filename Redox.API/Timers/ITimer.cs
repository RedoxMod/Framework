using System;

namespace Redox.API.Timers
{
    public interface ITimer : IDisposable
    {
        double Interval { get; }
        double TimeLeft { get; }

        int Repeat { get; }
        
        bool Finished { get; }
        
        TimerMode Mode { get; }
        
        Action CallBack { get; }
        
        void Start();

        void Stop();
    }
}