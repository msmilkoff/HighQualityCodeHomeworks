namespace ACTestingSystem.Interfaces
{
    using Models.Enums;

    /// <summary>
    /// Interface for the Report model.
    /// </summary>
    public interface IReport
    {
        /// <summary>
        /// The name of the AC Manufacturer.
        /// </summary>
        string Manufacturer { get; }

        /// <summary>
        /// The name of the AC Model.
        /// </summary>
        string Model { get; }

        /// <summary>
        /// Mark enum, for whether the test Passed of Failed.
        /// </summary>
        Mark Mark { get; }
    }
}
