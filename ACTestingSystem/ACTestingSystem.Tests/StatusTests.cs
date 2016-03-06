namespace ACTestingSystem.Tests
{
    using ACTestingSystem.Core;
    using ACTestingSystem.Core.Data;
    using ACTestingSystem.Interfaces;
    using ACTestingSystem.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StatusTests
    {
        private IController controller;
        private ITestingSystemDatabase data;

        [TestInitialize]
        public void Initialize()
        {
            this.data = new ACDatabase();
            this.controller = new Controller(this.data);
        }

        [TestMethod]
        public void Status_AllTested_ShouldReturn100Percent()
        {
            var h001 = new CarAirConditioner("Hitachi", "H001", 100);
            var h002 = new CarAirConditioner("Hitachi", "H002", 100);
            var h003 = new CarAirConditioner("Hitachi", "H003", 100);
            this.data.AddAirConditioner(h001);
            this.data.AddAirConditioner(h002);
            this.data.AddAirConditioner(h003);
            this.controller.TestAirConditioner("Hitachi", "H001");
            this.controller.TestAirConditioner("Hitachi", "H002");
            this.controller.TestAirConditioner("Hitachi", "H003");
            string actual = this.controller.Status();
            string expected = "Jobs complete: 100.00%";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Status_NoneTested_ShouldReturnZeroPercent()
        {
            var h001 = new CarAirConditioner("Hitachi", "H001", 100);
            var h002 = new CarAirConditioner("Hitachi", "H002", 100);
            var h003 = new CarAirConditioner("Hitachi", "H003", 100);
            this.data.AddAirConditioner(h001);
            this.data.AddAirConditioner(h002);
            this.data.AddAirConditioner(h003);
            string actual = this.controller.Status();
            string expected = "Jobs complete: 0.00%";

            Assert.AreEqual(expected, actual);
        }
    }
}
