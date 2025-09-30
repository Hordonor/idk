using System;

namespace Task5
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[][] groups = new int[][]
            {
                new int[] { 80, 90, 70, 60, 100 },
                new int[] { 50, 75, 80, 65, 95, 70 },
                new int[] { 90, 95, 100, 96 }
            };

            PrintGroupStatistics(groups);
        }

        public static double GetAverage(int[] marks)
        {
            double sum = 0;
            foreach (int x in marks)
                sum += x;
            return sum / marks.Length;
        }

        public static int GetMin(int[] marks)
        {
            int min = marks[0];
            foreach (int x in marks)
                if (x < min) min = x;
            return min;
        }

        public static int GetMax(int[] marks)
        {
            int max = marks[0];
            foreach (int x in marks)
                if (x > max) max = x;
            return max;
        }

        public static void PrintGroupStatistics(int[][] groups)
        {
            for (int i = 0; i < groups.Length; i++)
            {
                Console.WriteLine("Група " + (i + 1) +
                    ": Середній = " + GetAverage(groups[i]).ToString("F0") +
                    ", Мінімальний = " + GetMin(groups[i]) +
                    ", Максимальний = " + GetMax(groups[i]));
            }
        }
    }
}
