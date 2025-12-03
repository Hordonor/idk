using System;

namespace DeliverySystem
{
    public class Program
    {
        public static void Main()
        {
            // Створення об'єктів
            Vehicle scooter = new Scooter("Xiaomi", 2023, 1200, 30);
            Vehicle car = new Car("Toyota", 2021, 34000, 4);
            Vehicle van = new Van("Ford", 2020, 56000, 5, 1000);

            // Демонстрація роботи Scooter
            Console.WriteLine(scooter.GetInfo());
            Console.WriteLine($"Max speed: {scooter.GetMaxSpeed()} km/h");
            scooter.Move(20);
            ((Scooter)scooter).Charge(); // Приведення типу для доступу до методу Charge

            // Демонстрація роботи Car
            Console.WriteLine(car.GetInfo());
            Console.WriteLine($"Max speed: {car.GetMaxSpeed()} km/h");
            car.Move(50);

            // Демонстрація роботи Van
            Console.WriteLine(van.GetInfo());
            Console.WriteLine($"Max speed: {van.GetMaxSpeed()} km/h");
            ((Van)van).LoadCargo(800);   // Завантаження вантажу
            Console.WriteLine(van.GetInfo());
            ((Van)van).LoadCargo(300);   // Спроба перевантаження
            ((Van)van).UnloadCargo();    // Розвантаження

            Console.ReadLine();
        }
    }
}