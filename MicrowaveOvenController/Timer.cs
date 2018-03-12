using System;
using System.Timers;

namespace MicrowaveOvenController
{
    public class MicrowaveTimer : ITimer
    {
        private const double MICRO_IN_SEC = 1000;

        //could be moved as ctr arguments
        private const int Increase_interval_sec = 60;
        private const int initial_interval_sec = 60;

        private Timer _timer { get; set; }
        public MicrowaveTimer()
        {
            _timer = new Timer();
            _timer.Elapsed += InvokeElapsed;
        }

        private void InvokeElapsed(object sender, ElapsedEventArgs e)
        {
            var ev = Finished;
            if (ev != null) ev(this, e);
        }

        public void Start()
        {
            _timer.Interval = initial_interval_sec * MICRO_IN_SEC;
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Stop();

        }
        public void Increase()
        {
            _timer.Interval += Increase_interval_sec * MICRO_IN_SEC;
        }


        public event EventHandler Finished;


        public bool IsRunning
        {
            get { return _timer.Enabled; }
        }

    }
}
