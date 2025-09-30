using System;

namespace Task2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[] arr = GenerateRandomArray(10, 1, 100);

            Console.WriteLine("Наш масив:");
            foreach (int x in arr)
                Console.Write(x + " ");
            Console.WriteLine();

            Console.WriteLine("Сума = " + GetSum(arr));
            Console.WriteLine("Середнє = " + GetAverage(arr));
            Console.WriteLine("Мін = " + GetMin(arr));
            Console.WriteLine("Макс = " + GetMax(arr));
        }

        public static int[] GenerateRandomArray(int size, int min, int max)
        {
            int[] a = new int[size];
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                a[i] = r.Next(min, max + 1);
            }
            return a;
        }

        public static int GetSum(int[] a)
        {
            int s = 0;
            foreach (int x in a)
                s += x;
            return s;
        }

        public static double GetAverage(int[] a)
        {
            return (double)GetSum(a) / a.Length;
        }

        public static int GetMin(int[] a)
        {
            int min = a[0];
            foreach (int x in a)
                if (x < min) min = x;
            return min;
        }

        public static int GetMax(int[] a)
        {
            int max = a[0];
            foreach (int x in a)
                if (x > max) max = x;
            return max;
        }
    }
}
