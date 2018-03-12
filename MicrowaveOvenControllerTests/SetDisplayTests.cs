using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenController;
using Moq;

namespace MicrowaveOvenControllerTests
{
    /// <summary>
    /// Summary description for SetDisplayTests
    /// </summary>
    [TestClass]
    public class SetDisplayTests
    {
        [TestMethod]
        public void OpenedDisplayTest() 
        {
            var hardware = new Mock<IMicrowaveOvenHW>();
            var timer = new Mock<ITimer>();
            var controller = new MicrowaveController(hardware.Object, timer.Object, MicrowaveStateEnum.OPENED);

            Assert.AreEqual(true, controller.LightOn);
            Assert.AreEqual(false, controller.HeaterOn);
        }

        [TestMethod]
        public void ClosedDisplayTest()
        {
            var hardware = new Mock<IMicrowaveOvenHW>();
            var timer = new Mock<ITimer>();
            var controller = new MicrowaveController(hardware.Object, timer.Object, MicrowaveStateEnum.CLOSED);

            Assert.AreEqual(false, controller.LightOn);
            Assert.AreEqual(false, controller.HeaterOn);
        }

        [TestMethod]
        public void HeatingDisplayTest()
        {
            var hardware = new Mock<IMicrowaveOvenHW>();
            var timer = new Mock<ITimer>();
            var controller = new MicrowaveController(hardware.Object, timer.Object, MicrowaveStateEnum.HEATING);

            Assert.AreEqual(false, controller.LightOn);
            Assert.AreEqual(true, controller.HeaterOn);
        }
    }
}
