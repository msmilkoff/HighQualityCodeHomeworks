namespace ACTestingSystem.Tests
{
    using System;
    using Core;
    using Core.Data;
    using Exceptions;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Enums;

    [TestClass]
    public class RegisterTests
    {
        private IController controller;

        [TestInitialize]
        public void Initialize()
        {
            var data = new ACDatabase();
            this.controller = new Controller(data);
        }

        [TestMethod]
        public void RegisterStationaryAirConditioner_Success_ShouldReturnSuccessMessage()
        {
            string actual = this.controller.RegisterStationaryAirConditioner("Toshiba", "X1000", EfficiancyRating.A, 500);
            string expected = "Air Conditioner model X1000 from Toshiba registered successfully.";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterStationaryAirConditioner_IncorrectManufacturerLength_ShouldThrow()
        {
            this.controller.RegisterStationaryAirConditioner("c1", "X1000", EfficiancyRating.A, 500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterStationaryAirConditioner_IncorrectModelLength_ShouldThrow()
        {
            this.controller.RegisterStationaryAirConditioner("Lenovo", "X", EfficiancyRating.A, 500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterStationaryAirConditioner_NegativePowerUsage_ShouldThrow()
        {
            this.controller.RegisterStationaryAirConditioner("Lenovo", "GX2000", EfficiancyRating.B, -100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterStationaryAirConditioner_ZeroPowerUsage_ShouldThrow()
        {
            this.controller.RegisterStationaryAirConditioner("Lenovo", "GX2000", EfficiancyRating.B, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterStationaryAirConditioner_IncorrectRating_ShouldThrow()
        {
            EfficiancyRating rating = (EfficiancyRating)Enum.Parse(typeof(EfficiancyRating), "Z");
            this.controller.RegisterStationaryAirConditioner("Lenovo", "GX2000", rating, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateEntryException))]
        public void RegisterStationaryAirConditioner_DuplicateEntry_ShouldThrow()
        {
            this.controller.RegisterStationaryAirConditioner("Toshiba", "X1000", EfficiancyRating.A, 500);
            this.controller.RegisterStationaryAirConditioner("Lenovo", "X1000", EfficiancyRating.B, 300);
        }
    }
}