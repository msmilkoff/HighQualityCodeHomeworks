namespace ACTestingSystem.Models
{
    using System;
    using System.Text;
    using Interfaces;
    using Utilities;

    public class CarAirConditioner : AirConditioner, IVolumeCoverer
    {
        private int volumeCovered;

        public CarAirConditioner(string manufacturer, string model, int volumeCovered)
            : base(manufacturer, model)
        {
            this.VolumeCovered = volumeCovered;
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

        public override bool Test()
        {
            if (Math.Sqrt(this.VolumeCovered) < 3)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendLine(base.ToString());
            output.AppendLine(string.Format("Volume Covered: {0}", this.VolumeCovered));
            output.Append("====================");

            return output.ToString();
        }
    }
}