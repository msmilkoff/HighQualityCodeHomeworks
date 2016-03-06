namespace ACTestingSystem.Tests
{
    using System.IO;
    using System.Text;

    using ACTestingSystem.Core;
    using ACTestingSystem.Core.Data;
    using ACTestingSystem.Interfaces;
    using ACTestingSystem.Models;
    using ACTestingSystem.Models.Enums;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FindAllReportsTests
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
        public void FindAllReports_Suceess_ShouldDisplayAllReportsView()
        {
            this.data.AddReport(new Report("Toshiba", "001", Mark.Passed));
            this.data.AddReport(new Report("Toshiba", "002", Mark.Failed));
            this.data.AddReport(new Report("Toshiba", "003", Mark.Passed));

            string actual = this.controller.FindAllReportsByManufacturer("Toshiba");
            var expected = new StringBuilder();
            expected.Append("Reports from Toshiba:\r\n");
            expected.Append("Report\r\n");
            expected.Append("====================\r\n");
            expected.Append("Manufacturer: Toshiba\r\n");
            expected.Append("Model: 001\r\n");
            expected.Append("Mark: Passed\r\n");
            expected.Append("====================\r\n");
            expected.Append("Report\r\n");
            expected.Append("====================\r\n");
            expected.Append("Manufacturer: Toshiba\r\n");
            expected.Append("Model: 002\r\n");
            expected.Append("Mark: Failed\r\n");
            expected.Append("====================\r\n");
            expected.Append("Report\r\n");
            expected.Append("====================\r\n");
            expected.Append("Manufacturer: Toshiba\r\n");
            expected.Append("Model: 003\r\n");
            expected.Append("Mark: Passed\r\n");
            expected.Append("====================");

            Assert.AreEqual(expected.ToString(), actual);
            this.data.Reports.Clear();
        }

        [TestMethod]
        public void FindAllReports_NoReportsFromGivenManufacturer_ShoudReturnNoReportsMessage()
        {
            this.data.AddReport(new Report("LG Electronics", "LG001", Mark.Passed));
            this.data.AddReport(new Report("LG Electronics", "LG002", Mark.Failed));

            string actual = this.controller.FindAllReportsByManufacturer("Toshiba");
            string expected = "No reports.";

            Assert.AreEqual(expected, actual);
            this.data.Reports.Clear();
        }

        [TestMethod]
        public void FindAllReports_EmptyRepository_ShoudReturnNoReportsMessage()
        {
            string actual = this.controller.FindAllReportsByManufacturer("Toshiba");
            string expected = "No reports.";

            Assert.AreEqual(expected, actual);
        }
    }
}