using System;

namespace Task4
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Сторона a:");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine("Сторона b:");
            double b = double.Parse(Console.ReadLine());
            Console.WriteLine("Сторона c:");
            double c = double.Parse(Console.ReadLine());

            if (!IsValidTriangle(a, b, c))
            {
                Console.WriteLine("Трикутник не існує");
                return;
            }

            Console.WriteLine("Периметр = " + GetPerimeter(a, b, c));
            Console.WriteLine("Площа = " + GetArea(a, b, c));
            Console.WriteLine("Тип: " + GetTriangleType(a, b, c));
        }

        public static bool IsValidTriangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0) return false;
            if (a + b <= c || a + c <= b || b + c <= a) return false;
            return true;
        }

        public static double GetPerimeter(double a, double b, double c)
        {
            return a + b + c;
        }

        public static double GetArea(double a, double b, double c)
        {
            double p = GetPerimeter(a, b, c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static string GetTriangleType(double a, double b, double c)
        {
            if (a == b && b == c) return "рівносторонній";
            if (a == b || a == c || b == c) return "рівнобедрений";

            double[] sides = { a, b, c };
            Array.Sort(sides);
            if (Math.Abs(sides[2] * sides[2] - (sides[0] * sides[0] + sides[1] * sides[1])) < 0.0001)
                return "прямокутний";

            return "довільний";
        }
    }
}
