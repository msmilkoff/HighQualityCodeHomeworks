namespace BoatRacingSimulator.Models.Boats
{
    using BoatRacingSimulator.Interfaces;
    using BoatRacingSimulator.Utilities;

    public class Yacht : Boat
    {
        private int cargoWeight;

        public Yacht(string model, int weight, IBoatEngine engine, int cargoWeight)
            : base(model, weight)
        {
            this.Engine = engine;
            this.CargoWeight = cargoWeight;
            this.IsPowerBoat = true;
        }

        public IBoatEngine Engine { get; }

        public int CargoWeight
        {
            get
            {
                return this.cargoWeight;
            }

            private set
            {
                Validator.ValidatePropertyValue(value, "Cargo Weight");
                this.cargoWeight = value;
            }
        }

        public override bool IsPowerBoat { get; }

        public override double CalculateRaceSpeed(IRace race)
        {
            var weight = this.Weight + this.CargoWeight;
            var result = (this.Engine.Output - weight) + (race.OceanCurrentSpeed / 2);

            return result;
        }
    }
}