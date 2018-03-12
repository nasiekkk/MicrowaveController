using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenController;
using Moq;
using System;


namespace MicrowaveOvenControllerTests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void Closed_OpeningDoors()
        {
            var expectedState = MicrowaveStateEnum.OPENED;
            
            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(true);
            var timer = new Mock<ITimer>();
            var controller = new MicrowaveController(hardware.Object, timer.Object);

            hardware.Raise(el => el.DoorOpenChanged += null, false);

            Assert.AreEqual(false, controller.HeaterOn);
            Assert.AreEqual(true, controller.LightOn);
            Assert.AreEqual(expectedState, controller.MicrowaveState);
        }

        [TestMethod]
        public void Opened_ClosingDoors()
        {
            var expectedState = MicrowaveStateEnum.CLOSED;

            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(false);
            var timer = new Mock<ITimer>();
            var controller = new MicrowaveController(hardware.Object, timer.Object, MicrowaveStateEnum.OPENED);
            hardware.Raise(el => el.DoorOpenChanged += null, false);

            Assert.AreEqual(false, controller.HeaterOn);
            Assert.AreEqual(false, controller.LightOn);
            Assert.AreEqual(expectedState, controller.MicrowaveState);
        }
        [TestMethod]
        public void Closed_StartPressed()
        {
            var expectedState = MicrowaveStateEnum.HEATING;
            
            var hardware = new Mock<IMicrowaveOvenHW>();
            var timer = new Mock<ITimer>();
            var controller = new MicrowaveController(hardware.Object, timer.Object, MicrowaveStateEnum.CLOSED);
            hardware.Raise(el => el.StartButtonPressed += null, EventArgs.Empty);

            Assert.AreEqual(true, controller.HeaterOn);
            Assert.AreEqual(false, controller.LightOn);
            Assert.AreEqual(expectedState, controller.MicrowaveState);
            hardware.Verify(el => el.TurnOnHeater());
            timer.Verify(el => el.Start());
        }

        [TestMethod]
        public void Heating_OpeningDoors()
        {
            var expectedState = MicrowaveStateEnum.OPENED;

            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(true);
            var timer = new Mock<ITimer>();
            var controller = new MicrowaveController(hardware.Object, timer.Object, MicrowaveStateEnum.HEATING);
            hardware.Raise(el => el.DoorOpenChanged += null, true);

            Assert.AreEqual(false, controller.HeaterOn);
            Assert.AreEqual(true, controller.LightOn);
            Assert.AreEqual(expectedState, controller.MicrowaveState);
            timer.Verify(el => el.Stop());
            hardware.Verify(el => el.TurnOffHeater());
        }

        [TestMethod]
        public void Opened_StartPressed()
        {
            var expectedState = MicrowaveStateEnum.OPENED;

            var hardware = new Mock<IMicrowaveOvenHW>();
            var timer = new Mock<ITimer>();
            var controller = new MicrowaveController(hardware.Object, timer.Object, MicrowaveStateEnum.OPENED);
            hardware.Setup(el => el.DoorOpen).Returns(true);

            hardware.Raise(el => el.StartButtonPressed += null, EventArgs.Empty);

            Assert.AreEqual(false, controller.HeaterOn);
            Assert.AreEqual(true, controller.LightOn);
            Assert.AreEqual(expectedState, controller.MicrowaveState);

        }

        [TestMethod]
        public void Heating_StartPressed()
        {
            var expectedState = MicrowaveStateEnum.HEATING;

            var hardware = new Mock<IMicrowaveOvenHW>();
            var timer = new Mock<ITimer>();
            timer.Setup(el => el.IsRunning).Returns(true);
            var controller = new MicrowaveController(hardware.Object, timer.Object, MicrowaveStateEnum.HEATING);

            hardware.Raise(el => el.StartButtonPressed += null, EventArgs.Empty);

            timer.Verify(el => el.Increase());
            Assert.AreEqual(true, controller.HeaterOn);
            Assert.AreEqual(false, controller.LightOn);
            Assert.AreEqual(expectedState, controller.MicrowaveState);
        }

        [TestMethod]
        public void Heating_TimerFinished()
        {
            var expectedState = MicrowaveStateEnum.CLOSED;

            var hardware = new Mock<IMicrowaveOvenHW>();
            var timer = new Mock<ITimer>();
            timer.Setup(el => el.IsRunning).Returns(false);
            var controller = new MicrowaveController(hardware.Object, timer.Object, MicrowaveStateEnum.HEATING);

            timer.Raise(el => el.Finished += null, EventArgs.Empty);

            hardware.Verify(el => el.TurnOffHeater());
            Assert.AreEqual(false, controller.HeaterOn);
            Assert.AreEqual(false, controller.LightOn);
            Assert.AreEqual(expectedState, controller.MicrowaveState);
        }
    }
}
