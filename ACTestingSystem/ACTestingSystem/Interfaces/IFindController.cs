namespace ACTestingSystem.Interfaces
{
    public interface IFindController
    {
        string FindAirConditioner(string manufacturer, string model);

        string FindReport(string manufacturer, string model);

        string FindAllReportsByManufacturer(string manufacturer);
    }
}
