namespace BoatRacingSimulator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Database;
    using Enumerations;
    using Exceptions;
    using Interfaces;
    using Models.BoatEngines;
    using Models.Boats;
    using Models.Races;
    using Utilities;

    public class BoatSimulatorController : IBoatSimulatorController
    {
        public BoatSimulatorController(IDatabase database, IRace currentRace)
        {
            this.Database = database;
            this.CurrentRace = currentRace;
        }

        public BoatSimulatorController() : this(new BoatSimulatorDatabase(), null)
        {
        }

        public IRace CurrentRace { get; private set; }

        public IDatabase Database { get; }

        /// <summary>
        /// Creates a new boat engine and adds it to the database.
        /// </summary>
        /// <param name="model"> Engine manufacturer model </param>
        /// <param name="horsepower"> Engine power (in HP) </param>
        /// <param name="displacement"> Engine displacement (water draught) </param>
        /// <param name="engineType"> Engine type (Jet or Sterndrive)</param>
        /// <returns> A view with information about the outcome of the operation </returns>
        public string CreateBoatEngine(string model, int horsepower, int displacement, EngineType engineType)
        {
            IBoatEngine engine;
            switch (engineType)
            {
                case EngineType.Jet:
                    engine = new JetEngine(model, horsepower, displacement);
                    break;
                case EngineType.Sterndrive:
                    engine = new SterndriveEngine(model, horsepower, displacement);
                    break;
                default:
                    throw new NotImplementedException();
            }

            this.Database.Engines.Add(engine);
            return string.Format(
                "Engine model {0} with {1} HP and displacement {2} cm3 created successfully.",
                model,
                horsepower,
                displacement);
        }

        public string CreateRowBoat(string model, int weight, int oars)
        {
            IBoat boat = new RowBoat(model, weight, oars);
            this.Database.Boats.Add(boat);

            return string.Format("Row boat with model {0} registered successfully.", model);
        }

        public string CreateSailBoat(string model, int weight, int sailEfficiency)
        {
            IBoat boat = new SailBoat(model, weight, sailEfficiency);
            this.Database.Boats.Add(boat);

            return string.Format("Sail boat with model {0} registered successfully.", model);
        }

        public string CreatePowerBoat(string model, int weight, string firstEngineModel, string secondEngineModel)
        {
            IBoatEngine firstEngine = this.Database.Engines.GetItem(firstEngineModel);
            IBoatEngine secondEngine = this.Database.Engines.GetItem(secondEngineModel);
            IBoat boat = new PowerBoat(model, weight, firstEngine, secondEngine);
            this.Database.Boats.Add(boat);

            return string.Format("Power boat with model {0} registered successfully.", model);
        }

        public string CreateYacht(string model, int weight, string engineModel, int cargoWeight)
        {
            IBoatEngine engine = this.Database.Engines.GetItem(engineModel);
            IBoat boat = new Yacht(model, weight, engine, cargoWeight);
            this.Database.Boats.Add(boat);

            return string.Format("Yacht with model {0} registered successfully.", model);
        }

        public string OpenRace(int distance, int windSpeed, int oceanCurrentSpeed, bool allowsMotorboats)
        {
            IRace race = new Race(distance, windSpeed, oceanCurrentSpeed, allowsMotorboats);
            this.ValidateRaceIsEmpty();
            this.CurrentRace = race;
            return string.Format(
                "A new race with distance {0} meters, wind speed {1} m/s and ocean current speed {2} m/s has been set.",
                distance,
                windSpeed,
                oceanCurrentSpeed);
        }

        /// <summary>
        /// Adds a participant in the current race
        /// </summary>
        /// <param name="model"> The boat model </param>
        /// <returns> A view with information about the outcome of the operation </returns>
        public string SignUpBoat(string model)
        {
            IBoat boat = this.Database.Boats.GetItem(model);
            this.ValidateRaceIsSet();
            if (!this.CurrentRace.AllowsMotorboats && boat.IsPowerBoat)
            {
                throw new ArgumentException(Constants.IncorrectBoatTypeMessage);
            }

            this.CurrentRace.AddParticipant(boat);

            return string.Format("Boat with model {0} has signed up for the current Race.", model);
        }

        // PERFORMANCE: Performance bottleneck in redundant FindFastest method.
        // No need to itetate through "participants" 3 times.
        public string StartRace()
        {
            this.ValidateRaceIsSet();
            var participants = this.CurrentRace.GetParticipants();
            if (participants.Count < 3)
            {
                throw new InsufficientContestantsException(Constants.InsufficientContestantsMessage);
            }

            var winners = new List<KeyValuePair<double, IBoat>>();
            foreach (var boat in participants)
            {
                double raceSpeed = boat.CalculateRaceSpeed(this.CurrentRace);
                if (raceSpeed <= 0)
                {
                    winners.Add(new KeyValuePair<double, IBoat>(double.PositiveInfinity, boat));
                }
                else
                {
                    double time = this.CurrentRace.Distance / raceSpeed;
                    winners.Add(new KeyValuePair<double, IBoat>(time, boat));
                }
            }

            winners = winners.OrderBy(x => x.Key).ToList();
            var first = winners[0];
            var second = winners[1];
            var third = winners[2];

            var result = new StringBuilder();
            result.AppendLine(string.Format(
                "First place: {0} Model: {1} Time: {2}",
                first.Value.GetType().Name,
                first.Value.Model,
                double.IsInfinity(first.Key) ? "Did not finish!" : first.Key.ToString("0.00") + " sec"));
            result.AppendLine(string.Format(
                "Second place: {0} Model: {1} Time: {2}",
                second.Value.GetType().Name,
                second.Value.Model,
                double.IsInfinity(second.Key) ? "Did not finish!" : second.Key.ToString("0.00") + " sec"));
            result.Append(string.Format(
                "Third place: {0} Model: {1} Time: {2}",
                third.Value.GetType().Name,
                third.Value.Model,
                double.IsInfinity(third.Key) ? "Did not finish!" : third.Key.ToString("0.00") + " sec"));

            this.CurrentRace = null;

            return result.ToString();
        }

        public string GetStatistic()
        {
            var participants = this.CurrentRace.GetParticipants();
            var boatTypes = participants.Select(p => p.GetType().Name);

            var occurances = new SortedDictionary<string, int>();
            foreach (var boatType in boatTypes)
            {
                if (!occurances.ContainsKey(boatType))
                {
                    occurances.Add(boatType, 1);
                }
                else
                {
                    occurances[boatType]++;
                }
            }

            var result = new StringBuilder();
            foreach (var occurance in occurances)
            {
                var boatCount = participants.Count;
                double percent = (double)occurance.Value / boatCount * 100;

                if (occurance.Key == occurances.Last().Key)
                {
                    result.Append(string.Format("{0} -> {1:F2}%", occurance.Key, percent));
                }
                else
                {
                    result.AppendLine(string.Format("{0} -> {1:F2}%", occurance.Key, percent));
                }
            }

            return result.ToString();
        }

        private void ValidateRaceIsSet()
        {
            if (this.CurrentRace == null)
            {
                throw new NoSetRaceException(Constants.NoSetRaceMessage);
            }
        }

        private void ValidateRaceIsEmpty()
        {
            if (this.CurrentRace != null)
            {
                throw new RaceAlreadyExistsException(Constants.RaceAlreadyExistsMessage);
            }
        }
    }
}