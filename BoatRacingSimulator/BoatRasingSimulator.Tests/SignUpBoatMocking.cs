namespace BoatRasingSimulator.Tests
{
    using System;
    using BoatRacingSimulator.Controllers;
    using BoatRacingSimulator.Exceptions;
    using BoatRacingSimulator.Interfaces;
    using BoatRacingSimulator.Models.BoatEngines;
    using BoatRacingSimulator.Models.Boats;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class SignUpBoatMocking
    {
        private Mock<IDatabase> fakeData;
        private IBoatSimulatorController controller;
        private Mock<IRace> fakeRace;

        [TestInitialize]
        public void InitializeMocks()
        {
            var dataMock = new Mock<IDatabase>();
            var boatsMock = new Mock<IRepository<IBoat>>();
            var raceMock = new Mock<IRace>();

            boatsMock
                .Setup(x => x.GetItem("model"))
                .Returns(new RowBoat("model", 100, 2));
            dataMock.Setup(d => d.Boats.GetItem("model"))
                .Returns(new RowBoat("model", 100, 2));
            dataMock.Setup(d => d.Boats.GetItem("motor1"))
                .Returns(new Yacht("motor1", 100, new JetEngine("jetPower", 500, 300), 100));
            raceMock
                .Setup(r => r.AllowsMotorboats)
                .Returns(false);

            this.fakeData = dataMock;
            this.fakeRace = raceMock;
        }

        [TestMethod]
        public void SignUpBoat_SignUp_ShouldGetCorrectItem()
        {
            this.controller = new BoatSimulatorController(this.fakeData.Object, this.fakeRace.Object);
            this.controller.SignUpBoat("model");

            this.fakeData.Verify(fd => fd.Boats.GetItem("model"), Times.Exactly(1));
        }

        [TestMethod]
        [ExpectedException(typeof(NoSetRaceException))]
        public void SignUpBoat_NoSetRace_ShouldThrow()
        {
            this.controller = new BoatSimulatorController(this.fakeData.Object, null);
            try
            {
                this.controller.SignUpBoat("model");
            }
            catch (NoSetRaceException ex)
            {
                Assert.AreEqual("There is currently no race set.", ex.Message);
                throw;
            }
            finally
            {
                Assert.IsNull(this.controller.CurrentRace);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SignUpBoat_SignUpMotorBoat_NoMotorBoatsAllowed_ShouldThrow()
        {
            this.controller = new BoatSimulatorController(this.fakeData.Object, this.fakeRace.Object);
            try
            {
                this.controller.SignUpBoat("motor1");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("The specified boat does not meet the race constraints.", ex.Message);
                throw;
            }
            finally
            {
                this.fakeRace.Verify(r => r.AllowsMotorboats, Times.Exactly(1));
            }
        }

        [TestMethod]
        public void SignUpBoat_WithRaceSet_ShouldAddParticipantToCurrentRace()
        {
            this.controller = new BoatSimulatorController(this.fakeData.Object, this.fakeRace.Object);
            this.controller.SignUpBoat("model");

            this.fakeRace.Verify(r => r.AddParticipant(It.IsAny<IBoat>()), Times.Exactly(1));
        }
        
        [TestMethod]
        public void SignUpBoat_Success_ShouldReturnSuccessMessage()
        {
            this.controller = new BoatSimulatorController(this.fakeData.Object, this.fakeRace.Object);
            string actual = this.controller.SignUpBoat("model");
            string expected = "Boat with model model has signed up for the current Race.";

            Assert.AreEqual(expected, actual);
        }
    }
}