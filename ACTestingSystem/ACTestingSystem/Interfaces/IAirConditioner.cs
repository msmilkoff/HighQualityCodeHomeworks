namespace ACTestingSystem.Interfaces
{
    public interface IAirConditioner : ITestable
    {
        string Manufacturer { get; }

        string Model { get; }
    }
}
