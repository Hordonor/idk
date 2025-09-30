using System;

namespace Task3
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Введіть свій вік:");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine(ClassifyAge(age));
        }

        public static string ClassifyAge(int age)
        {
            if (age < 0 || age > 120)
                return "Нереальний вік";
            else if (age <= 11)
                return "Ви дитина";
            else if (age <= 17)
                return "Підліток";
            else if (age <= 59)
                return "Дорослий";
            else
                return "Пенсіонер";
        }
    }
}
