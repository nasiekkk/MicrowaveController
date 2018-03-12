namespace MicrowaveOvenController
{
    class Program
    {
        static void Main(string[] args)
        {
            IMicrowaveOvenHW hardware = new MicrowaveOvenHW();
            ITimer timer = new MicrowaveTimer();
            MicrowaveController microwave = new MicrowaveController(hardware, timer);


        }
    }
}