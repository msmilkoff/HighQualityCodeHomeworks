namespace BoatRacingSimulator.Interfaces
{
    public interface IDatabase
    {
        IRepository<IBoat> Boats { get; }

        IRepository<IBoatEngine> Engines { get; }
    }
}
