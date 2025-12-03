using System;

namespace DeliverySystem
{
    public class Car : Vehicle
    {
        protected int doors;
        protected double fuelLevel;

        // Публічний конструктор для звичайних авто (швидкість 180)
        public Car(string brand, int year, double mileage, int doors)
            : this(brand, year, mileage, doors, 180.0)
        {
        }

        // Захищений конструктор для нащадків (наприклад, Van), які мають іншу швидкість
        protected Car(string brand, int year, double mileage, int doors, double maxSpeed)
            : base(brand, year, mileage, maxSpeed)
        {
            this.doors = doors;
            this.fuelLevel = 50;
        }

        public override string GetInfo()
        {
            return $"Car: {brand} ({year}), Doors: {doors}, Fuel: {fuelLevel}L";
        }

        public override void Move(double distance)
        {
            base.Move(distance);

            double consumption = distance * 0.1;
            fuelLevel -= consumption;

            if (fuelLevel < 0)
            {
                fuelLevel = 0;
            }
        }

        public void Refuel(double liters)
        {
            fuelLevel += liters;
            if (fuelLevel > 50)
            {
                fuelLevel = 50;
            }
        }
    }
}