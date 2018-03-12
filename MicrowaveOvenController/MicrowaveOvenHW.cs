using System;

namespace MicrowaveOvenController
{
    public class MicrowaveOvenHW : IMicrowaveOvenHW
    {

        public void TurnOnHeater()
        {
            Console.WriteLine("Heater turned on");
        }

        public void TurnOffHeater()
        {
            Console.WriteLine("Heater turned off");
        }

        private bool _doorOpen;
        public bool DoorOpen
        {
            get { return _doorOpen; }
        }


        public event Action<bool> DoorOpenChanged;

        public event EventHandler StartButtonPressed;
    }
}
