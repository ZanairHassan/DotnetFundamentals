using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement_09
{
    public class Bus : Vehicle
    {
        public int SeatingCapacity { get; private set; }

        public Bus(int id, string brand, string model, int seatingCapacity)
            : base(id, brand, model)
        {
            if (seatingCapacity <= 0)
                throw new ArgumentException("seatingCapacity must be greater than 0.");
            SeatingCapacity = seatingCapacity;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine("\n******* Bus Details *******");

            base.DisplayDetails();

            Console.WriteLine($"Seating Capacity: {SeatingCapacity}");
        }
    }
}
