using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions_Homework
{
    public class Exceptions
    {
        public static T[] Subsequence<T>(T[] arr, int startIndex, int count)
        {
            List<T> result = new List<T>();
            for (int i = startIndex; i < startIndex + count; i++)
            {
                result.Add(arr[i]);
            }
            return result.ToArray();
        }

        public static string ExtractEnding(string str, int count)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (count > str.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Invalid count");
            }

            StringBuilder result = new StringBuilder();
            for (int i = str.Length - count; i < str.Length; i++)
            {
                result.Append(str[i]);
            }

            return result.ToString();
        }

        public static bool IsPrime(int number)
        {
            bool isPrime = true;
            for (int divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    isPrime =  false;
                }
            }

            return isPrime;
        }

        static void Main()
        {
            var substr = Subsequence("Hello!".ToCharArray(), 2, 3);
            Console.WriteLine(substr);

            var subarr = Subsequence(new[] { -1, 3, 2, 1 }, 0, 2);
            Console.WriteLine(String.Join(" ", subarr));

            var allarr = Subsequence(new[] { -1, 3, 2, 1 }, 0, 4);
            Console.WriteLine(String.Join(" ", allarr));

            var emptyarr = Subsequence(new[] { -1, 3, 2, 1 }, 0, 0);
            Console.WriteLine(String.Join(" ", emptyarr));

            Console.WriteLine(ExtractEnding("I love C#", 2));
            Console.WriteLine(ExtractEnding("Nakov", 4));
            Console.WriteLine(ExtractEnding("beer", 4));
          //Console.WriteLine(ExtractEnding("Hi", 100)); throws exception : Invalid count

            int number = 23;
            Console.WriteLine(IsPrime(number) 
                ? $"{number} is prime" 
                : $"{number} is not prime");

            number = 33;
            Console.WriteLine(IsPrime(number)
            ? $"{number} is prime"
            : $"{number} is not prime");

            List<IExam> peterExams = new List<IExam>()
            {
                new SimpleMathExam(2),
                new CSharpExam(55),
                new CSharpExam(100),
                new SimpleMathExam(1),
                new CSharpExam(0),
            };

            Student peter = new Student("Peter", "Petrov");
            peter.Exams = peterExams;

            double peterAverageResult = peter.CalcAverageExamResultInPercents();
            Console.WriteLine($"{peter.FirstName} {peter.LastName}: Average results = { peterAverageResult:p0}");
        }
    }
}
