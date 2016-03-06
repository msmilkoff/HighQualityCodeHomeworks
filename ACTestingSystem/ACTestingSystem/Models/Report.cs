namespace ACTestingSystem.Models
{
    using System;
    using System.Text;
    using Enums;
    using Interfaces;

    public class Report : IReport
    {
        public Report(string manufacturer, string model, Mark mark)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Mark = mark;
        }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public Mark Mark { get; set; }

        public override string ToString() 
        {
            // PERFORMANCE: Performance bottleneck when finding all reports - too many string concatanations.
            var output = new StringBuilder();

            output.AppendLine("Report");
            output.AppendLine("====================");
            output.AppendLine(string.Format("Manufacturer: {0}", this.Manufacturer));
            output.AppendLine(string.Format("Model: {0}", this.Model));
            output.AppendLine(string.Format("Mark: {0}", this.Mark));
            output.Append("====================");

            return output.ToString();
        }
    }
}
