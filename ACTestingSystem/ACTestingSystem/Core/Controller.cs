namespace ACTestingSystem.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using Exceptions;
    using Interfaces;
    using Models;
    using Models.Enums;
    using Utilities;

    public class Controller : IController
    {
        public Controller(ITestingSystemDatabase database)
        {
            this.Database = database;
        }

        public ITestingSystemDatabase Database { get; }

        public string RegisterStationaryAirConditioner(string manufacturer, string model, EfficiancyRating energyEfficiencyRating, int powerUsage)
        {
            var airConditioner = new StationaryAirConditioner(manufacturer, model, energyEfficiencyRating, powerUsage);

            this.Database.AddAirConditioner(airConditioner);

            string result = string.Format(Messages.RegisterAc, airConditioner.Model, airConditioner.Manufacturer);
            return result;
        }

        public string RegisterCarAirConditioner(string manufacturer, string model, int volumeCoverage)
        {
            var airConditioner = new CarAirConditioner(manufacturer, model, volumeCoverage);

            this.Database.AddAirConditioner(airConditioner);
            string result = string.Format(Messages.RegisterAc, airConditioner.Model, airConditioner.Manufacturer);

            return result;
        }

        /// <summary>
        /// Creates a new Plane AC and adds it to the databse
        /// </summary>
        /// <param name="manufacturer"> AC Manufacturer</param>
        /// <param name="model"> AC Model</param>
        /// <param name="volumeCoverage"> Volume covered</param>
        /// <param name="electricityUsed"> Electricity usage</param>
        /// <returns>
        /// A (string) message, containing information on whether the command succeded,
        /// and if not - description of what went wrong.
        /// </returns>
        public string RegisterPlaneAirConditioner(string manufacturer, string model, int volumeCoverage, int electricityUsed)
        {
            var airConditioner = new PlaneAirConditioner(manufacturer, model, volumeCoverage, electricityUsed);

            this.Database.AddAirConditioner(airConditioner);
            string result = string.Format(Messages.RegisterAc, airConditioner.Model, airConditioner.Manufacturer);

            return result;
        }

        public string TestAirConditioner(string manufacturer, string model)
        {
            if (this.Database.Reports.ContainsKey(model)
                    && this.Database.Reports[model].Manufacturer == manufacturer)
            {
                throw new DuplicateEntryException(Messages.DuplicateEntry);
            }

            var airConditioner = this.Database.FindAirConditioner(manufacturer, model);
            var hasPassed = airConditioner.Test();
            var mark = hasPassed ? Mark.Passed : Mark.Failed;

            IReport report = new Report(airConditioner.Manufacturer, airConditioner.Model, mark);
            this.Database.AddReport(report);

            string result = string.Format(Messages.Tested, model, manufacturer);

            return result;
        }

        /// <summary>
        /// Extract an air conditioner from the repository.
        /// </summary>
        /// <param name="manufacturer"> AC Manufacturer </param>
        /// <param name="model"> AC Model </param>
        /// <returns>
        /// A message containing info about the air conditioner,
        /// or an error message in case such entry was not found
        /// </returns>
        public string FindAirConditioner(string manufacturer, string model)
        {
            var airConditioner = this.Database.FindAirConditioner(manufacturer, model);
            string result = airConditioner.ToString();

            return result;
        }

        public string FindReport(string manufacturer, string model)
        {
            var report = this.Database.GetReport(manufacturer, model);
            string result = report.ToString();

            return result;
        }

        public string FindAllReportsByManufacturer(string manufacturer)
        {
            string result;

            var reports = this.Database.GetReportsByManufacturer(manufacturer);
            if (reports == null)
            {
                result = Messages.NoReports;
            }
            else
            {
                reports = reports.OrderBy(x => x.Model);

                var reportsPrint = new StringBuilder();
                reportsPrint.AppendLine(string.Format("Reports from {0}:", manufacturer));
                reportsPrint.Append(string.Join(Environment.NewLine, reports));
                result = reportsPrint.ToString();
            }

            return result;
        }

        /// <summary>
        /// Generates a message displaying the status of the system (percentage of air conditioners tested). 
        /// </summary>
        /// <returns>
        /// A message displaying the percentage of registered air conditioners that were tested.
        /// </returns>
        public string Status()
        {
            int reports = this.Database.GetReportsCount();
            double airConditioners = this.Database.GetAirConditionersCount();

            double percent = reports / airConditioners;
            percent = percent * 100;
            if (double.IsNaN(percent))
            {
                percent = 0.00;
            }

            return string.Format(Messages.Status, percent);
        }
    }
}