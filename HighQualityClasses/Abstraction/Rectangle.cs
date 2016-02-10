namespace Abstraction
{
    using System;

    class Rectangle : IFigure
    {
        private double width;
        private double height;
        
        public Rectangle(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        public double Width
        {
            get { return this.width; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Width must be positive.");
                }

                this.width = value;
            }
        }

        public double Height
        {
            get { return this.height; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Height must be positive.");
                }

                this.height = value;
            }
        }

        public double CalculatePerimeter()
        {
            double perimeter = 2 * (this.Width + this.Height);

            return perimeter;
        }

        public double CalculateSurface()
        {
            double surface = this.Width * this.Height;

            return surface;
        }
    }
}
