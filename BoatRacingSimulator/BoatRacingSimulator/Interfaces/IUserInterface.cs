namespace BoatRacingSimulator.Interfaces
{
    /// <summary>
    /// This interface just combines <see cref="IReader"/> and <see cref="IWriter"/>,
    /// without adding new functionality
    /// </summary>
    public interface IUserInterface : IReader, IWriter
    {
    }
}