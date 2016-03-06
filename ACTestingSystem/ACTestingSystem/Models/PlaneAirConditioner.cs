namespace ACTestingSystem.Models
{
    using System;
    using System.Text;
    using Interfaces;
    using Utilities;

    public class PlaneAirConditioner : AirConditioner, IVolumeCoverer
    {
        private int volumeCovered;
        private int electricityUsed;

        public PlaneAirConditioner(string manufacturer, string model, int volumeCovered, int electricityUsed)
            : base(manufacturer, model)
        {
            this.VolumeCovered = volumeCovered;
            this.ElectricityUsed = electricityUsed;
        }

        public int VolumeCovered
        {
            get
            {
                return this.volumeCovered;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Messages.AssertPositive, "Volume Covered"));
                }

                this.volumeCovered = value;
            }
        }

        public int ElectricityUsed
        {
            get
            {
                return this.electricityUsed;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Messages.AssertPositive, "Electricity Used"));
                }

                this.electricityUsed = value;
            }
        }

        public override bool Test()
        {
            var requirement = this.ElectricityUsed / Math.Sqrt(this.VolumeCovered);
            if (requirement < 150)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendLine(base.ToString());
            output.AppendLine(string.Format("Volume Covered: {0}", this.VolumeCovered));
            output.AppendLine(string.Format("Electricity Used: {0}", this.ElectricityUsed));
            output.Append("====================");

            return output.ToString();
        }
    }
}
