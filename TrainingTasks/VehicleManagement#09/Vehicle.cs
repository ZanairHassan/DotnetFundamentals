using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement_09
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }


        public Vehicle(int id, string brand, string model)
        {
            Id = id;
            Brand = brand;
            Model = model;
        }

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"ID : {Id}");
            Console.WriteLine($"Brand : {Brand}");
            Console.WriteLine($"Model : {Model}");
        }
    }
}
