using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement_09
{
    public class Truck : Vehicle
    {
        public double LoadCapacity { get; private set; }

        public Truck(int id, string brand, string model, double loadCapacity) : base(id, brand, model)
        {
            if (loadCapacity <= 0)
                throw new ArgumentException("loadCapacity must be greater than 0.");
            LoadCapacity = loadCapacity;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine("\n****** Truck Details ******");

            base.DisplayDetails();

            Console.WriteLine($"Load Capacity: {LoadCapacity} Tons");
        }
    }
}
