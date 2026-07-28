using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement_09
{
    public class Vehicle
    {
        public int Id { get; private set; }
        public string Brand { get;private set; }
        public string Model { get; private set; }


        public Vehicle(int id, string brand, string model)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than 0.");

            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Brand cannot be empty.");

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be empty.");

            Id = id;
            Brand = brand.Trim();
            Model = model.Trim();
        }

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"ID : {Id}");
            Console.WriteLine($"Brand : {Brand}");
            Console.WriteLine($"Model : {Model}");
        }
    }
}
