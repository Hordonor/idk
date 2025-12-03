using System;
using System.Text;

namespace SmartHomeSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // 1. Створити контролер
            SmartHomeController controller = new SmartHomeController();

            // 2. Створити пристрої (без конструкторів, через ініціалізатори)
            Light lamp = new Light { Name = "Лампа у вітальні" };
            AirConditioner ac = new AirConditioner { Name = "Кондиціонер у спальні" };
            CoffeeMachine coffeeMachine = new CoffeeMachine { Name = "Кавомашина на кухні" };
            MotionSensor motionSensor = new MotionSensor { Name = "Датчик руху у коридорі" };

            // 3. Додати пристрої до контролера
            // Всі як ISwitchable
            controller.AddDevice(lamp);
            controller.AddDevice(ac);
            controller.AddDevice(coffeeMachine);
            controller.AddDevice(motionSensor);

            // Ті, що споживають енергію, як IEnergyConsumer
            controller.AddEnergyDevice(lamp);
            controller.AddEnergyDevice(ac);
            controller.AddEnergyDevice(coffeeMachine);

            // 4. Демонстрація роботи
            // Вмикаємо всі
            controller.TurnAllOn();
            Console.WriteLine();

            // Статус кожного
            lamp.PrintStatus();
            ac.PrintStatus();
            coffeeMachine.PrintStatus();
            motionSensor.PrintStatus();
            Console.WriteLine();

            // Звіт
            controller.ShowEnergyReport(5);
            Console.WriteLine();

            // Вимикаємо всі
            controller.TurnAllOff();

            Console.ReadLine();
        }
    }
}