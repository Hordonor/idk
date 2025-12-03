using System;
using System.Collections.Generic;

namespace SmartHomeSystem
{
    public class SmartHomeController
    {
        private readonly List<ISwitchable> _devices = new List<ISwitchable>();
        private readonly List<IEnergyConsumer> _energyDevices = new List<IEnergyConsumer>();

        public void AddDevice(ISwitchable device)
        {
            _devices.Add(device);
        }

        public void AddEnergyDevice(IEnergyConsumer device)
        {
            _energyDevices.Add(device);
        }

        public void TurnAllOn()
        {
            foreach (var device in _devices)
            {
                device.TurnOn();
            }
        }

        public void TurnAllOff()
        {
            foreach (var device in _devices)
            {
                device.TurnOff();
            }
        }

        public void ShowEnergyReport(int hours)
        {
            Console.WriteLine($"Звіт про споживання енергії за {hours} год:");

            double totalEnergy = 0;

            foreach (var device in _energyDevices)
            {
                double usage = device.GetEnergyUsage(hours);
                totalEnergy += usage;
                Console.WriteLine($"{device.DeviceName}: {usage:F2} кВт·год (потужність: {device.PowerConsumption} Вт)");
            }

            Console.WriteLine($"Загальне споживання: {totalEnergy:F2} кВт·год");
            Console.WriteLine($"Вартість (~4 грн/кВт·год): {totalEnergy * 4:F2} грн");
        }
    }
}