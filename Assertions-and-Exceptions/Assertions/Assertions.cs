using System;

namespace Assertions_Homework
{
    class Assertions
    {
        public static void SelectionSort<T>(T[] arr) where T : IComparable<T>
        {
            if (arr == null || arr.Length == 0 || arr.Length == 1)
            {
                throw new ArgumentException("Array must have more than one element");
            }

            for (int index = 0; index < arr.Length - 1; index++)
            {
                int minElementIndex = FindMinElementIndex(arr, index, arr.Length - 1);
                Swap(ref arr[index], ref arr[minElementIndex]);
            }
        }
  
        private static int FindMinElementIndex<T>(T[] arr, int startIndex, int endIndex) 
            where T : IComparable<T>
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr), "Array is null");
            }
            if (arr.Length == 0)
            {
                return 0;
            }

            ValidateIndeces(arr, startIndex, endIndex);

            int minElementIndex = startIndex;
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                if (arr[i].CompareTo(arr[minElementIndex]) < 0)
                {
                    minElementIndex = i;
                }
            }
            return minElementIndex;
        }

        private static void ValidateIndeces<T>(T[] arr, int startIndex, int endIndex)
        {
            if (startIndex < 0 || startIndex >= arr.Length)
            {
                throw new IndexOutOfRangeException("Index is outisde the boundaries of the array");
            }
            if (endIndex <= startIndex)
            {
                throw new IndexOutOfRangeException("End index cannot be less than or equal to Start ndex");
            }
        }

        public static int BinarySearch<T>(T[] arr, T value) where T : IComparable<T>
        {
            return BinarySearch(arr, value, 0, arr.Length - 1);
        }

        private static int BinarySearch<T>(T[] arr, T value, int startIndex, int endIndex) 
            where T : IComparable<T>
        {
            while (startIndex <= endIndex)
            {
                int midIndex = (startIndex + endIndex) / 2;
                if (arr[midIndex].Equals(value))
                {
                    return midIndex;
                }
                if (arr[midIndex].CompareTo(value) < 0)
                {
                    // Search on the right half
                    startIndex = midIndex + 1;
                }
                else 
                {
                    // Search on the right half
                    endIndex = midIndex - 1;
                }
            }

            // Searched value not found
            return -1;
        }

        static void Main()
        {
            int[] arr = new int[] { 3, -1, 15, 4, 17, 2, 33, 0 };
            Console.WriteLine("arr = [{0}]", string.Join(", ", arr));
            SelectionSort(arr);
            Console.WriteLine("sorted = [{0}]", string.Join(", ", arr));

            //SelectionSort(new int[0]); // Throws exception

            //SelectionSort(new int[1]); // Throws exception

            int result = BinarySearch(arr, -1000);
            AssertBinaryResult(result);

            result = BinarySearch(arr, 0);
            AssertBinaryResult(result);

            result = BinarySearch(arr, 17);
            AssertBinaryResult(result);

            result = BinarySearch(arr, 10);
            AssertBinaryResult(result);

            result = BinarySearch(arr, 1000);
            AssertBinaryResult(result);
        }

        private static void AssertBinaryResult(int binarySearchResult)
        {
            if (binarySearchResult == -1)
            {
                Console.WriteLine("Value not found");
            }
            else
            {
                Console.WriteLine(binarySearchResult);
            }
        }

        private static void Swap<T>(ref T x, ref T y)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x), "Values cannot be null");
            }
            if (y == null)
            {
                throw new ArgumentNullException(nameof(y), "Values cannot be null");
            }

            T oldX = x;
            x = y;
            y = oldX;
        }
    }
}
