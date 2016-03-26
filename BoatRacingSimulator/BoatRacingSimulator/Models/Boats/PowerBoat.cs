namespace BoatRacingSimulator.Models.Boats
{
    using System.Collections.Generic;
    using System.Linq;
    using BoatRacingSimulator.Interfaces;

    public class PowerBoat : Boat
    {
        private const int Factor = 5;

        public PowerBoat(string model, int weight, IBoatEngine firstEngine, IBoatEngine secondEngine)
            : base(model, weight)
        {
            this.Engines = new List<IBoatEngine> { firstEngine, secondEngine };
            this.IsPowerBoat = true;
        }

        public ICollection<IBoatEngine> Engines { get; }

        public override bool IsPowerBoat { get; }

        public override double CalculateRaceSpeed(IRace race)
        {
            double output = this.Engines.Sum(e => e.Output);
            double result = (output - this.Weight) + (race.OceanCurrentSpeed / (double)Factor);

            return result;
        }
    }
}