namespace Abstraction
{
    using System;

    class Circle : IFigure
    {
        private double radius;

        public Circle(double radius)
        {
            this.Radius = radius;
        }

        public double Radius
        {
            get { return this.radius; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Radius must be positive.");
                }

                this.radius = value;
            }
        }

        public double CalculatePerimeter()
        {
            double perimeter = 2 * Math.PI * this.Radius;

            return perimeter;
        }

        public double CalculateSurface()
        {
            double area = Math.PI * radius * radius;

            return area;
        }
    }
}
