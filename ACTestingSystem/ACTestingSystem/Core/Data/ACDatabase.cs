namespace ACTestingSystem.Core.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Interfaces;
    using Utilities;

    public class ACDatabase : ITestingSystemDatabase
    {
        public ACDatabase()
        {
            this.AirConditioners = new Dictionary<string, IAirConditioner>();
            this.Reports = new Dictionary<string, IReport>();
        }

        /* PERFORMANCE: Using Lists as main repositories results in worse performance in most operations - O(n),
        compared to Dictionaries - O(1) */

        public IDictionary<string, IAirConditioner> AirConditioners { get; set; }

        public IDictionary<string, IReport> Reports { get; set; }

        public void AddAirConditioner(IAirConditioner airConditioner)
        {
            if (this.AirConditioners.ContainsKey(airConditioner.Model))
            {
                throw new DuplicateEntryException(Messages.DuplicateEntry);
            }

            this.AirConditioners.Add(airConditioner.Model, airConditioner);
        }

        public IAirConditioner FindAirConditioner(string manufacturer, string model)
        {
            if (this.AirConditioners.ContainsKey(model)
                && this.AirConditioners[model].Manufacturer == manufacturer)
            {
                return this.AirConditioners[model];
            }

            throw new NonExistantEntryException(Messages.NonExist);
        }

        public int GetAirConditionersCount()
        {
            return this.AirConditioners.Count;
        }

        public void AddReport(IReport report)
        {
            this.Reports.Add(report.Model, report);
        }

        public IReport GetReport(string manufacturer, string model)
        {
            if (this.Reports.ContainsKey(model)
                && this.Reports[model].Manufacturer == manufacturer)
            {
                return this.Reports[model];
            }

            throw new NonExistantEntryException(Messages.NonExist);
        }

        public int GetReportsCount()
        {
            return this.Reports.Count;
        }

        public IEnumerable<IReport> GetReportsByManufacturer(string manufacturer)
        {
            if (this.Reports.Any(x => x.Value.Manufacturer == manufacturer))
            {
                var reports = this.Reports.Values.Where(x => x.Manufacturer == manufacturer);
                return reports;
            }

            return null;
        }
    }
}