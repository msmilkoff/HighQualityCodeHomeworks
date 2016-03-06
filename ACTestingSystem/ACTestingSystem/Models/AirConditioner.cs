namespace ACTestingSystem.Models
{
    using System;
    using System.Text;
    using Interfaces;
    using Utilities;

    public abstract class AirConditioner : IAirConditioner
    {
        private string manufacturer;
        private string model;

        protected AirConditioner(string manufacturer, string model)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
        }

        public string Manufacturer
        {
            get
            {
                return this.manufacturer;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < Constants.ManufacturerMinLength)
                {
                    string msg = string.Format(
                        "Manufacturer's name must be at least {0} symbols long.",
                        Constants.ManufacturerMinLength);

                    throw new ArgumentException(msg);
                }

                this.manufacturer = value;
            }
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < Constants.ModelMinLength)
                {
                    throw new ArgumentException(string.Format(
                        "Model's name must be at least {0} symbols long.",
                        Constants.ModelMinLength));
                }

                this.model = value;
            }
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendLine("Air Conditioner");
            output.AppendLine("====================");
            output.AppendLine(string.Format("Manufacturer: {0}", this.Manufacturer));
            output.Append(string.Format("Model: {0}", this.Model));

            return output.ToString();
        }

        public abstract bool Test();
    }
}