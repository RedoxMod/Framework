using System;
using System.Timers;
using Redox.API.Timers;

namespace Redox.Core.Timers
{
    public sealed class Timer : ITimer
    {
        public double Interval { get;}
        
        public double TimeLeft { get; }
        public int Repeat { get; }

        public bool Finished { get; private set; }
        public TimerMode Mode { get; }
        
        public Action CallBack { get; }
        
        private System.Timers.Timer _timer;

        private int _repeated;

        public void Start()
        {
            _timer = new System.Timers.Timer()
            {
                Interval = this.Interval, 
                AutoReset = IsRepeatingTimer(this.Mode)

            };
            _timer.Elapsed += this.HandleTimer;
            _timer.Start();
        }

        public void Stop()
        {
            if (_timer != null && _timer.Enabled)
            {
                this.Dispose();
            }
        }
        
        private void HandleTimer(object sender, ElapsedEventArgs e)
        {
            CallBack.Invoke();

            if (IsRepeatingTimer(this.Mode))
            {
                if (_repeated == Repeat)
                    this.Stop();
                else
                {
                    _repeated++;
                }
            }
        }
        
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Timer(TimerMode mode, double interval, Action callBack)
        {
            this.Mode = mode;
            this.Interval = interval;
            this.CallBack = callBack;
        }

        public Timer(TimerMode mode, double interval, Action callBack, int repeat)
        {
            this.Mode = mode;
            this.Interval = interval;
            this.CallBack = callBack;
            this.Repeat = repeat;
        }

        private static bool IsRepeatingTimer(TimerMode mode)
        {
            return mode == TimerMode.Repeating || mode == TimerMode.Infinite;
        }
    }
}