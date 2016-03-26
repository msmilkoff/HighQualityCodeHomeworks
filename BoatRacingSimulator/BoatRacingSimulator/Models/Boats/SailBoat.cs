namespace BoatRacingSimulator.Models.Boats
{
    using System;
    using BoatRacingSimulator.Interfaces;
    using BoatRacingSimulator.Utilities;

    public class SailBoat : Boat
    {
        private const int Factor = 100;

        private int sailEfficiency;

        public SailBoat(string model, int weight, int sailEfficiency)
            : base(model, weight)
        {
            this.SailEfficiency = sailEfficiency;
            this.IsPowerBoat = false;
        }

        public int SailEfficiency
        {
            get
            {
                return this.sailEfficiency;
            }

            private set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentException(Constants.IncorrectSailEfficiencyMessage);
                }

                this.sailEfficiency = value;
            }
        }

        public override bool IsPowerBoat { get; }

        public override double CalculateRaceSpeed(IRace race)
        {
            var efficiancyFactor = this.sailEfficiency / (double)Factor;
            var result = ((race.WindSpeed * efficiancyFactor) - this.Weight) + (race.OceanCurrentSpeed / 2.0);

            return result;
        }
    }
}