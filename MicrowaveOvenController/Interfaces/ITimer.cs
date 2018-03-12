using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MicrowaveOvenController
{
    public interface ITimer
    {
        void Start();
        void Stop();
        void Increase();

        event EventHandler Finished;

        bool IsRunning{ get;}
    }
}
