using System;

namespace MicrowaveOvenController
{
    public class MicrowaveController
    {
        
        private MicrowaveStateEnum _microwaveState = MicrowaveStateEnum.CLOSED;

        public MicrowaveStateEnum MicrowaveState
        {
            get { return _microwaveState; }
            private set
            {
                _microwaveState = value;
                HandleTimer();
                SetDisplay();
            }
        }

        private IMicrowaveOvenHW _hardware;
        private ITimer _timer;
        private bool _heaterOn;

        public bool HeaterOn
        {
            get { return _heaterOn; }
            private set
            {
                if (_heaterOn == value)
                    return;
                _heaterOn = value;
                if (_heaterOn)
                {
                    _hardware.TurnOnHeater();
                }
                else
                {
                    _hardware.TurnOffHeater();
                }
            }
        }
        private bool _lightOn;

        public bool LightOn
        {
            get { return _lightOn; }
            private set { _lightOn = value; }
        }


        public MicrowaveController(IMicrowaveOvenHW hardware, ITimer timer, MicrowaveStateEnum initialState = MicrowaveStateEnum.CLOSED)
        {
            if (hardware == null || timer == null)
                throw new Exception("Provide hardware and timer");

            _hardware = hardware;
            _hardware.StartButtonPressed += StartButtonPressed;
            _hardware.DoorOpenChanged += DoorOpenedChanged;
            
            _timer = timer;
            _timer.Finished += TimerStopped;
     
            MicrowaveState = initialState;
        }

        private void SetDisplay(){
            LightOn = _microwaveState == MicrowaveStateEnum.OPENED;
            HeaterOn = _microwaveState == MicrowaveStateEnum.HEATING;
        }
    
        private void HandleTimer(){
            if (_microwaveState != MicrowaveStateEnum.HEATING)
            {
                _timer.Stop();
                
            }
            else
            {
                if (HeaterOn)
                {
                    _timer.Increase();
                }
                else
                {
                    _timer.Start();
                }            
            }
        }
        private void StartButtonPressed(object sender, System.EventArgs e)
        {
            MicrowaveState = GetNextState(_microwaveState, true);
            
        }

        private void DoorOpenedChanged(bool flag){

            MicrowaveState = GetNextState(_microwaveState, false);
        }

        private void TimerStopped(object sender, System.EventArgs e)
        {
            MicrowaveState = GetNextState(_microwaveState, false);
        }

        public MicrowaveStateEnum GetNextState(MicrowaveStateEnum currentState, bool startPressed)
        {
            switch (currentState)
            {
                case MicrowaveStateEnum.CLOSED:
                    if (_hardware.DoorOpen)
                    {
                        return MicrowaveStateEnum.OPENED;
                    }
                    if (startPressed == true)
                    {
                        return MicrowaveStateEnum.HEATING;
                    }
                    return MicrowaveStateEnum.CLOSED;
                    
                case MicrowaveStateEnum.OPENED:
                    if (!_hardware.DoorOpen)
                    {
                        return MicrowaveStateEnum.CLOSED;
                    }
                    return MicrowaveStateEnum.OPENED;
                    
                case MicrowaveStateEnum.HEATING:
                    if (_hardware.DoorOpen)
                    {
                        return MicrowaveStateEnum.OPENED;
                    }
                    if (!_timer.IsRunning)
                    {
                        return MicrowaveStateEnum.CLOSED;
                    }
                    return MicrowaveStateEnum.HEATING;
            }
            throw new UnhandledStateException("Unexpected state: " + currentState);

        }

    }
}
