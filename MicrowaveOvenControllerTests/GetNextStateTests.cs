using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenController;
using Moq;

namespace MicrowaveOvenControllerTests
{
    [TestClass]
    public class GetNextStateTests
    {

        [TestMethod]
        public void GetNextStateTestClosedToClosed()
        {
            var expectedState = MicrowaveStateEnum.CLOSED;

            var hardware = new Mock<IMicrowaveOvenHW>();
            //hardware.Setup(el => el.DoorOpen).Returns(false);
            var timer = new Mock<ITimer>();
            //timer.Setup(el => el.CustomTimer).Returns(new System.Timers.Timer(1.0));
            var controller = new MicrowaveController(hardware.Object, timer.Object);
            var result = controller.GetNextState(MicrowaveStateEnum.CLOSED, false);
            Assert.AreEqual(expectedState, result);
        }

        [TestMethod]
        public void GetNextStateTestClosedToOpened()
        {
            var expectedState = MicrowaveStateEnum.OPENED;

            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(true);
            var timer = new Mock<ITimer>();
            //timer.Setup(el => el.CustomTimer).Returns(new System.Timers.Timer(1.0));
            var controller = new MicrowaveController(hardware.Object, timer.Object);
            var result = controller.GetNextState(MicrowaveStateEnum.CLOSED, false);
            Assert.AreEqual(expectedState, result);
        }

        [TestMethod]
        public void GetNextStateTestClosedToHeating()
        {
            var expectedState = MicrowaveStateEnum.HEATING;

            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(false);
            var timer = new Mock<ITimer>();
            //timer.Setup(el => el.CustomTimer).Returns(new System.Timers.Timer(1.0));
            var controller = new MicrowaveController(hardware.Object, timer.Object);
            var result = controller.GetNextState(MicrowaveStateEnum.CLOSED, true);
            Assert.AreEqual(expectedState, result);
        }

        [TestMethod]
        public void GetNextStateTestOpenedToOpened()
        {
            var expectedState = MicrowaveStateEnum.OPENED;

            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(true);
            var timer = new Mock<ITimer>();
            //timer.Setup(el => el.CustomTimer).Returns(new System.Timers.Timer(1.0));
            var controller = new MicrowaveController(hardware.Object, timer.Object);
            var result = controller.GetNextState(MicrowaveStateEnum.OPENED, false);
            Assert.AreEqual(expectedState, result);
        }

        [TestMethod]
        public void GetNextStateTestOpenedToClosed()
        {
            var expectedState = MicrowaveStateEnum.CLOSED;
            
            var hardware = new Mock<IMicrowaveOvenHW>();
            //hardware.Setup(el => el.DoorOpen).Returns(false);
            var timer = new Mock<ITimer>();
            //timer.Setup(el => el.Is).Returns(new System.Timers.Timer(1.0));
            var controller = new MicrowaveController(hardware.Object, timer.Object);
            var result = controller.GetNextState(MicrowaveStateEnum.OPENED, false);
            Assert.AreEqual(expectedState, result);
        }

        [TestMethod]
        public void GetNextStateTestHeatingToHeating1()
        {
            var expectedState = MicrowaveStateEnum.HEATING;
            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(false);
            var timer = new Mock<ITimer>();
            timer.Setup(el => el.IsRunning).Returns(true);
            var controller = new MicrowaveController(hardware.Object, timer.Object);
            var result = controller.GetNextState(MicrowaveStateEnum.HEATING, true);
            Assert.AreEqual(expectedState, result);
        }

        public void GetNextStateTestHeatingToHeating2()
        {
            var expectedState = MicrowaveStateEnum.HEATING;
            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(false);
            var timer = new Mock<ITimer>();
            //timer.Setup(el => el.CustomTimer).Returns(new System.Timers.Timer(1.0));
            var controller = new MicrowaveController(hardware.Object, timer.Object);
            var result = controller.GetNextState(MicrowaveStateEnum.HEATING, false);
            Assert.AreEqual(expectedState, result);
        }

        [TestMethod]
        public void GetNextStateTestHeatingToOpened()
        {
            var expectedState = MicrowaveStateEnum.OPENED;
            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(true);
            var timer = new Mock<ITimer>();
            //timer.Setup(el => el.CustomTimer).Returns(new System.Timers.Timer(1.0));
            var controller = new MicrowaveController(hardware.Object, timer.Object);
            var result = controller.GetNextState(MicrowaveStateEnum.HEATING, false);
            Assert.AreEqual(expectedState, result);
        }

        [TestMethod]
        public void GetNextStateTestHeatingToClosed()
        {
            var expectedState = MicrowaveStateEnum.CLOSED;
            var hardware = new Mock<IMicrowaveOvenHW>();
            hardware.Setup(el => el.DoorOpen).Returns(false);
            var timer = new Mock<ITimer>();
            //timer.Setup(el => el.CustomTimer).Returns(new System.Timers.Timer(1.0));
            var controller = new MicrowaveController(hardware.Object, timer.Object);
            var result = controller.GetNextState(MicrowaveStateEnum.HEATING, false);
            Assert.AreEqual(expectedState, result);
        }
    }
}
