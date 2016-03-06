namespace ACTestingSystem.Interfaces
{
    using Models.Enums;

    public interface IRegistrationController
    {
        string RegisterStationaryAirConditioner(
            string manufacturer,
            string model,
            EfficiancyRating efficiancyRating,
            int powerUsage);

        string RegisterCarAirConditioner(string manufacturer, string model, int volumeCoverage);

        string RegisterPlaneAirConditioner(string manufacturer, string model, int vlumeCoverage, int electricityUsed);
    }
}
