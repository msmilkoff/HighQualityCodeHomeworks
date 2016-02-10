using System;

namespace Methods
{
    public static class Methods
    {
        public static double CalcTriangleArea(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentException("Triangle sides shoud be positive.");
            }

            if (a + b < c ||
                a + c < b ||
                b + c < a)
            {
                throw new ArgumentException("The length of a side must always be less the the sum of the other two.");
            }

            double semiPerimeter = (a + b + c) / 2;
            double area = Math.Sqrt(
                semiPerimeter * (semiPerimeter - a) * (semiPerimeter - b) * (semiPerimeter - c));

            return area;
        }

        public static string SpellDigit(int digitCandidate)
        {
            switch (digitCandidate)
            {
                case 0: return "zero";
                case 1: return "one";
                case 2: return "two";
                case 3: return "three";
                case 4: return "four";
                case 5: return "five";
                case 6: return "six";
                case 7: return "seven";
                case 8: return "eight";
                case 9: return "nine";
            }

            throw new ArgumentException(digitCandidate + " is not a digit");
        }

        public static int FindMax(params int[] elements)
        {
            if (elements == null || elements.Length == 0)
            {
                throw new InvalidOperationException("The collection has no elements");
            }
            if (elements.Length == 1)
            {
                return elements[0];
            }

            for (int i = 1; i < elements.Length; i++)
            {
                if (elements[i] > elements[0])
                {
                    elements[0] = elements[i];
                }
            }

            return elements[0];
        }

        public static void FormatNumber(object number, string format)
        {
            if (format == "f")
            {
                Console.WriteLine("{0:f2}", number); //float
            }
            if (format == "%")
            {
                Console.WriteLine("{0}%", number); //percentage
            }
            if (format == "r")
            {
                Console.WriteLine("{0,5}", number); //align right
            }
        }

        public static double CalcDistance(double x1, double y1, double x2, double y2,
            out bool isHorizontal, out bool isVertical)
        {
            //comparison to 5th digit after the decimal point
            isHorizontal = (Math.Abs(y1 - y2) < 0.00001);
            isVertical = (Math.Abs(x1 - x2) < 0.00001);

            double xCoordsSquared = (x2 - x1) * (x2 - x1);
            double yCoordsSquared = (y2 - y1) * (y2 - y1);
            double distance = Math.Sqrt(xCoordsSquared + yCoordsSquared);

            return distance;
        }

        public static void Main()
        {
            //Console.WriteLine(CalcTriangleArea(3, 4, 5));

            //Console.WriteLine(SpellDigit(5));

            //Console.WriteLine(FindMax(5, -1, 3, 2, 14, 2, 3));

            //FormatNumber(1.3, "f");
            //FormatNumber(0.75, "%");
            //FormatNumber(2.30, "r");

            //bool horizontal, vertical;
            //Console.WriteLine(CalcDistance(3, -1, 3, 2.5, out horizontal, out vertical));
            //Console.WriteLine(horizontal ? "Hrizontal" : "Vertical");

            Student peter = new Student
            {
                FirstName = "Peter",
                LastName = "Ivanov",
                BirthDay = new DateTime(1993, 05, 11),
                OtherInfo = "From Sofia"
            };

            Student stella = new Student
            {
                FirstName = "Stella",
                LastName = "Markova",
                BirthDay = new DateTime(1992, 02, 06),
                OtherInfo = "From Vidin, gamer, high results"
            };

            Console.WriteLine(peter.IsOlderThan(stella)
                ? $"{peter.FirstName} is older than {stella.FirstName}"
                : $"{peter.FirstName} is not older than {stella.FirstName}");
        }
    }
}
