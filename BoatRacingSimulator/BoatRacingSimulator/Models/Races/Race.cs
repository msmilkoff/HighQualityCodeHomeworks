namespace BoatRacingSimulator.Models.Races
{
    using System.Collections.Generic;
    using Exceptions;
    using Interfaces;
    using Utilities;

    public class Race : IRace
    {
        private int distance;

        public Race(int distance, int windSpeed, int oceanCurrentSpeed, bool allowsMotorboats)
        {
            this.Distance = distance;
            this.WindSpeed = windSpeed;
            this.OceanCurrentSpeed = oceanCurrentSpeed;
            this.AllowsMotorboats = allowsMotorboats;
            this.RegisteredBoats = new Dictionary<string, IBoat>();
        }

        public int Distance
        {
            get
            {
                return this.distance;
            }

            private set
            {
                Validator.ValidatePropertyValue(value, "Distance");
                this.distance = value;
            }
        }

        public int WindSpeed { get; }

        public int OceanCurrentSpeed { get; }

        public bool AllowsMotorboats { get; }

        public IDictionary<string, IBoat> RegisteredBoats { get; }

        public void AddParticipant(IBoat boat)
        {
            if (this.RegisteredBoats.ContainsKey(boat.Model))
            {
                throw new DuplicateModelException(Constants.DuplicateModelMessage);
            }

            this.RegisteredBoats.Add(boat.Model, boat);
        }
         
        // PERFORMANCE: Possible sluggish performance. No need to cast to IList.
        // We use ICollection and return the ValueCollection instead.
        public ICollection<IBoat> GetParticipants()
        {
            return this.RegisteredBoats.Values;
        }
    }
}