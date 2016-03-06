namespace ACTestingSystem.Models
{
    using System;
    using System.Text;
    using Enums;
    using Utilities;

    public class StationaryAirConditioner : AirConditioner
    {
        private int powerUsage;

        public StationaryAirConditioner(
            string manufacturer,
            string model,
            EfficiancyRating requiredEfficiancyRating,
            int powerUsage)
            : base(manufacturer, model)
        {
            this.PowerUsage = powerUsage;
            this.RequiredEfficiancyRating = requiredEfficiancyRating;
            this.ActualEfficiancyRating = this.GetRating(powerUsage);
        }

        public int PowerUsage
        {
            get
            {
                return this.powerUsage;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Messages.AssertPositive, "Power Usage"));
                }

                this.powerUsage = value;
            }
        }

        public EfficiancyRating RequiredEfficiancyRating { get; }

        public EfficiancyRating ActualEfficiancyRating { get; }

        public override bool Test()
        {
            if (this.ActualEfficiancyRating <= this.RequiredEfficiancyRating)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendLine(base.ToString());
            output.AppendLine(string.Format("Required energy efficiency rating: {0}", this.RequiredEfficiancyRating));
            output.AppendLine(string.Format("Power Usage(KW / h): {0}", this.PowerUsage));
            output.Append("====================");

            return output.ToString();
        }

        private EfficiancyRating GetRating(int powerUsed)
        {
            if (powerUsed < 1000)
            {
                return EfficiancyRating.A;
            }
            else if (powerUsed >= 1000 && powerUsed <= 1250)
            {
                return EfficiancyRating.B;
            }
            else if (powerUsed >= 1251 && powerUsed <= 1500)
            {
                return EfficiancyRating.C;
            }
            else if (powerUsed >= 1501 && powerUsed <= 2000)
            {
                return EfficiancyRating.D;
            }

            return EfficiancyRating.E;
        }
    }
}