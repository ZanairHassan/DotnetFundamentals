using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement_09
{
    public class Bike : Vehicle
    {
        public bool HasGear { get; set; }

        public Bike(int id, string brand, string model, bool hasGear)
            : base(id, brand, model)
        {
            HasGear = hasGear;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine("\n******** Bike Details ********");

            base.DisplayDetails();

            Console.WriteLine($"Has Gear : {(HasGear ? "Yes" : "No")}");
        }
    }
}
