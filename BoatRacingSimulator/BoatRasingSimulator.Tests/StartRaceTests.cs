namespace BoatRasingSimulator.Tests
{
    using System.Text;

    using BoatRacingSimulator.Controllers;
    using BoatRacingSimulator.Exceptions;
    using BoatRacingSimulator.Interfaces;
    using BoatRacingSimulator.Models.Boats;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StartRaceTests
    {
        private IBoatSimulatorController controller;
        [TestInitialize]
        public void InitializeTests()
        {
            this.controller = new BoatSimulatorController();
        }

        [TestMethod]
        [ExpectedException(typeof(NoSetRaceException))]
        public void StartRace_NoSetRace_ShouldThrow()
        {
            try
            {
                this.controller.StartRace();
            }
            catch (NoSetRaceException ex)
            {
                Assert.AreEqual("There is currently no race set.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientContestantsException))]
        public void StartRace_InsufficientContestants_ShouldThrow()
        {
            var first = new RowBoat("rowboat1", 100, 4);
            var second = new RowBoat("rowboat2", 90, 4);

            this.controller.OpenRace(5000, 10, 5, false);
            this.controller.CurrentRace.AddParticipant(first);
            this.controller.CurrentRace.AddParticipant(second);

            this.controller.StartRace();
        }

        [TestMethod]
        public void StartRace_Race_ShouldOrderWinnersByTime()
        {
            var first = new RowBoat("rowboat1", 80, 4);
            var second = new RowBoat("rowboat2", 90, 4);
            var third = new RowBoat("rowboat3", 100, 4);

            this.controller.OpenRace(5000, 10, 5, false);
            this.controller.CurrentRace.AddParticipant(first);
            this.controller.CurrentRace.AddParticipant(second);
            this.controller.CurrentRace.AddParticipant(third);

            var distance = this.controller.CurrentRace.Distance;
            var firstTime = distance / first.CalculateRaceSpeed(this.controller.CurrentRace);
            var secondTime = distance / second.CalculateRaceSpeed(this.controller.CurrentRace);
            var thirdTime = distance / third.CalculateRaceSpeed(this.controller.CurrentRace);

            var expected = new StringBuilder();
            expected.AppendLine(string.Format(
                "First place: RowBoat Model: rowboat1 Time: {0} sec",
                firstTime.ToString("0.00")));
            expected.AppendLine(string.Format(
                "Second place: RowBoat Model: rowboat2 Time: {0} sec",
                secondTime.ToString("0.00")));
            expected.Append(string.Format(
                "Third place: RowBoat Model: rowboat3 Time: {0} sec",
                thirdTime.ToString("0.00")));

            var actual = this.controller.StartRace();

            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestMethod]
        public void StartRace_BoatWithNegativeTime_ShouldNotFinish()
        {
            var first = new RowBoat("rowboat1", 80, 4);
            var second = new RowBoat("rowboat2", 90, 4);
            var third = new SailBoat("sailboat1", 200, 98);

            this.controller.OpenRace(1000, 10, 5, false);
            this.controller.CurrentRace.AddParticipant(first);
            this.controller.CurrentRace.AddParticipant(second);
            this.controller.CurrentRace.AddParticipant(third);

            var distance = this.controller.CurrentRace.Distance;
            var firstTime = distance / first.CalculateRaceSpeed(this.controller.CurrentRace);
            var secondTime = distance / second.CalculateRaceSpeed(this.controller.CurrentRace);
            var thirdTime = distance / third.CalculateRaceSpeed(this.controller.CurrentRace);
            if (thirdTime <= 0)
            {
                thirdTime = double.PositiveInfinity;
            }

            var expected = new StringBuilder();
            expected.AppendLine(string.Format(
                "First place: RowBoat Model: rowboat1 Time: {0} sec",
                firstTime.ToString("0.00")));
            expected.AppendLine(string.Format(
                "Second place: RowBoat Model: rowboat2 Time: {0} sec",
                secondTime.ToString("0.00")));
            expected.Append("Third place: SailBoat Model: sailboat1 Time: ");
            expected.Append(double.IsInfinity(thirdTime) ? "Did not finish!" : thirdTime.ToString("0.00"));

            var actual = this.controller.StartRace();

            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestMethod]
        public void StartRace_FinishRace_ShouldRemoveCurrentRaceAfterFinish()
        {
            var first = new RowBoat("rowboat1", 80, 4);
            var second = new RowBoat("rowboat2", 90, 4);
            var third = new SailBoat("sailboat1", 200, 98);

            this.controller.OpenRace(1000, 10, 5, false);
            this.controller.CurrentRace.AddParticipant(first);
            this.controller.CurrentRace.AddParticipant(second);
            this.controller.CurrentRace.AddParticipant(third);

            this.controller.StartRace();

            Assert.IsNull(this.controller.CurrentRace);
        }

        [TestMethod]
        public void StartRace_BoatsWithEqualTime_ShouldDisplayInOrderOfAddition()
        {
            var first = new RowBoat("SecondAdded", 80, 2);
            var second = new RowBoat("FirstAdded", 80, 2);
            var third = new RowBoat("rowboat3", 100, 2);

            this.controller.OpenRace(5000, 10, 5, false);
            this.controller.CurrentRace.AddParticipant(second);
            this.controller.CurrentRace.AddParticipant(first);
            this.controller.CurrentRace.AddParticipant(third);

            var distance = this.controller.CurrentRace.Distance;
            var firstTime = distance / first.CalculateRaceSpeed(this.controller.CurrentRace);
            var secondTime = distance / second.CalculateRaceSpeed(this.controller.CurrentRace);
            var thirdTime = distance / third.CalculateRaceSpeed(this.controller.CurrentRace);

            var expected = new StringBuilder();
            expected.AppendLine(string.Format(
                "First place: RowBoat Model: FirstAdded Time: {0} sec",
                secondTime.ToString("0.00")));
            expected.AppendLine(string.Format(
                "Second place: RowBoat Model: SecondAdded Time: {0} sec",
                firstTime.ToString("0.00")));
            expected.Append(string.Format(
                "Third place: RowBoat Model: rowboat3 Time: {0} sec",
                thirdTime.ToString("0.00")));

            var actual = this.controller.StartRace();

            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}