namespace BoatRasingSimulator.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using BoatRacingSimulator.Controllers;
    using BoatRacingSimulator.Exceptions;
    using BoatRacingSimulator.Interfaces;
    using BoatRacingSimulator.Models.Boats;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OpenRaceTests
    {
        private IBoatSimulatorController controller;

        [TestInitialize]
        public void InitializeTests()
        {
            this.controller = new BoatSimulatorController();
        }

        [TestMethod]
        public void OpenRace_Success_ShouldReturnSuccessMessage()
        {
            string actual = this.controller.OpenRace(5000, 10, 5, true);
            string expected =
                "A new race with distance 5000 meters, wind speed 10 m/s and ocean current speed 5 m/s has been set.";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RaceAlreadyExistsException))]
        public void OpenRace_AlredySet_ShouldThrow()
        {
            this.controller.OpenRace(2000, 7, 3, false);
            try
            {
                this.controller.OpenRace(2500, 5, 2, false);
            }
            catch (RaceAlreadyExistsException ex)
            {
                Assert.AreEqual("The current race has already been set.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void OpenRace_SetRace_ShouldSetRaceNotNull()
        {
            this.controller.OpenRace(3, 2, 1, true);
            Assert.AreNotEqual(this.controller.CurrentRace, null);
        }

        [TestMethod]
        public void OpenRace_SetRace_ShouldSetRaceWithCorrectParameters()
        {
            this.controller.OpenRace(10, 5, 2, false);

            Assert.AreEqual(10, this.controller.CurrentRace.Distance);
            Assert.AreEqual(5, this.controller.CurrentRace.WindSpeed);
            Assert.AreEqual(2, this.controller.CurrentRace.OceanCurrentSpeed);
            Assert.AreEqual(false, this.controller.CurrentRace.AllowsMotorboats);
        }

        [TestMethod]
        public void OpenRace_GetParticipants_ShouldReturnCorrectParticipants()
        {
            var first = new SailBoat("model1", 100, 45);
            var second = new SailBoat("model2", 100, 45);
            var third = new SailBoat("model3", 100, 45);

            var expected = new List<IBoat> { first, second, third };

            this.controller.OpenRace(500, 2, 1, false);
            this.controller.CurrentRace.AddParticipant(first);
            this.controller.CurrentRace.AddParticipant(second);
            this.controller.CurrentRace.AddParticipant(third);

            var actual = this.controller.CurrentRace.GetParticipants().ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}