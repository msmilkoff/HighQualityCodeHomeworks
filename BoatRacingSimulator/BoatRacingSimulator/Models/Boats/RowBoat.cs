namespace BoatRacingSimulator.Models.Boats
{
    using BoatRacingSimulator.Interfaces;
    using BoatRacingSimulator.Utilities;

    public class RowBoat : Boat
    {
        private const int Multiplier = 100;

        private int oars;

        public RowBoat(string model, int weight, int oars)
            : base(model, weight)
        {
            this.Oars = oars;
            this.IsPowerBoat = false;
        }

        public int Oars
        {
            get
            {
                return this.oars;
            }

            private set
            {
                Validator.ValidatePropertyValue(value, "Oars");
                this.oars = value;
            }
        }

        public override bool IsPowerBoat { get; }

        public override double CalculateRaceSpeed(IRace race)
        {
            double result = (this.Oars * Multiplier) - this.Weight + race.OceanCurrentSpeed;

            return result;
        }
    }
}