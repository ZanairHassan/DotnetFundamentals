using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement_09
{
    public class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public Car(int id, string brand, string model, int doors)
            : base(id, brand, model)
        {
            NumberOfDoors = doors;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine("\n****** Car Details ******");

            base.DisplayDetails();

            Console.WriteLine($"Doors : {NumberOfDoors}");
        }
    }
}
