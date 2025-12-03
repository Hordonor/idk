using System;

namespace SmartHomeSystem
{
    // MotionSensor не реалізує IEnergyConsumer, оскільки не споживає енергію
    public class MotionSensor : Device
    {
        public override void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"{Name} активовано.");
        }

        public override void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"{Name} деактивовано.");
        }
    }
}