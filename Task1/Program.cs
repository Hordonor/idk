using System;

namespace Task1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Введіть число:");
            int number = int.Parse(Console.ReadLine());

            string result = GetMessage(number);

            Console.WriteLine(result);
        }

        public static bool IsEven(int num)
        {
            if (num % 2 == 0)
                return true;
            else
                return false;
        }

        public static string GetMessage(int num)
        {
            if (IsEven(num))
                return "Двері відкриваються!";
            else
                return "Двері зачинені...";
        }
    }
}
