namespace ACTestingSystem.Interfaces
{
    using System.Collections.Generic;

    public interface IReportDatabaseManager
    {
        void AddReport(IReport report);

        IReport GetReport(string manufacturer, string model);

        int GetReportsCount();

        IEnumerable<IReport> GetReportsByManufacturer(string manufacturer);
    }
}
