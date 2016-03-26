namespace BoatRacingSimulator.Interfaces
{
    public interface IBoat : IModelable
    {
        int Weight { get; }

        bool IsPowerBoat { get; }

        double CalculateRaceSpeed(IRace race);
    }
}
