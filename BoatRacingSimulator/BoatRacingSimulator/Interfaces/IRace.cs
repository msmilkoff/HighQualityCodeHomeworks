namespace BoatRacingSimulator.Interfaces
{
    using System.Collections.Generic;
    using BoatRacingSimulator.Models.Races;

    /// <summary>
    /// Interface for the <see cref="Race"/> model.
    /// </summary>
    public interface IRace
    {
        /// <summary>
        /// Distance from start to finish.
        /// </summary>
        int Distance { get; }

        /// <summary>
        /// Current wind speed (m/s)
        /// </summary>
        int WindSpeed { get; }

        /// <summary>
        /// Ocean current speed (m/s)
        /// </summary>
        int OceanCurrentSpeed { get; }

        /// <summary>
        /// Determines if motorboats are allowed in the race
        /// </summary>
        bool AllowsMotorboats { get; }

        /// <summary>
        /// Adds a boat to the collection of participant for the current race
        /// </summary>
        /// <param name="boat"> The boat (that has signed up) to be added</param>
        void AddParticipant(IBoat boat);

        /// <summary>
        /// Returns all paricipants of the current race.
        /// </summary>
        /// <returns> The collection of current participants </returns>
        ICollection<IBoat> GetParticipants();
    }
}