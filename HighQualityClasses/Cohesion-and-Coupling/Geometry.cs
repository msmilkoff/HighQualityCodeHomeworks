namespace CohesionAndCoupling
{
    using System;

    public static class Geometry
    {
        public static double CalcDistance2D(double x1, double y1, double x2, double y2)
        {
            double distance = Math.Sqrt(
                (x2 - x1) * (x2 - x1) +
                (y2 - y1) * (y2 - y1));

            return distance;
        }

        public static double CalcDistance3D(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            double distance = Math.Sqrt(
                (x2 - x1) * (x2 - x1) +
                (y2 - y1) * (y2 - y1) +
                (z2 - z1) * (z2 - z1));

            return distance;
        }

        public static double CalcVolume(double width, double height, double depth)
        {
            ValidateDimensions3D(width, height, depth);

            double volume = width * height * depth;

            return volume;
        }

        public static double CalcDiagonalXYZ(double width, double height, double depth)
        {
            ValidateDimensions3D(width, height, depth);
            double distance = CalcDistance3D(0, 0, 0, width, height, depth);

            return distance;
        }

        public static double CalcDiagonalXY(double width, double height)
        {
            ValidateDimensions2D(width, height);
            double distance = CalcDistance2D(0, 0, width, height);

            return distance;
        }

        public static double CalcDiagonalXZ(double width, double depth)
        {
            const double x1 = 0;
            const double y1 = 0;

            ValidateDimensions2D(width, depth);
            double distance = CalcDistance2D(x1, y1, width, depth);

            return distance;
        }

        public static double CalcDiagonalYZ(double height, double depth)
        {
            const double x1 = 0;
            const double y1 = 0;

            ValidateDimensions2D(height, depth);
            double distance = CalcDistance2D(x1, y1, height, depth);

            return distance;
        }

        private static void ValidateDimensions3D(double x, double y, double z)
        {
            if (x <= 0 || y <= 0 || z <= 0)
            {
                throw new ArgumentException("Dimensions must be positive");
            }
        }

        private static void ValidateDimensions2D(double x, double y)
        {
            if (x <= 0 || y <= 0)
            {
                throw new ArgumentException("Dimensions must be positive");
            }
        }
    }
}
