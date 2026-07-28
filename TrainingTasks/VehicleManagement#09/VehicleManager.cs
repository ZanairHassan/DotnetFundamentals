using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace VehicleManagement_09
{
    public class VehicleManager
    {
        private List<Vehicle> vehicles = new List<Vehicle>();

        public void AddVehicle(Vehicle vehicle)
        {
            try
            {
                if(vehicle != null)
                {
                    Vehicle existingVehicle = vehicles.FirstOrDefault(v => v.Id == vehicle.Id);
                    if(existingVehicle != null)
                    {
                        Console.WriteLine("Vehicle with this id already exist.");
                        return;
                    }
                    else
                    {
                        vehicles.Add(vehicle);
                        Console.WriteLine("Vehicle Added Successfully.");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
           
        }

        public void DisplayVehicles()
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("No vehicles found.");
                return;
            }

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.DisplayDetails();
            }
        }

        public void SearchVehicleById(int id)
        {
            Vehicle vehicle = vehicles.FirstOrDefault(v => v.Id == id);

            if (vehicle != null)
            {
                Console.WriteLine("\nVehicle Found:");
                vehicle.DisplayDetails();
            }
            else
            {
                Console.WriteLine("Vehicle not found.");
            }
        }

        public void SearchVehicleByBrand(string brand)
        {
            List<Vehicle> result = vehicles.FindAll(v =>
                v.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));

            if (result.Count > 0)
            {
                Console.WriteLine($"\nVehicles of Brand: {brand}");

                foreach (Vehicle vehicle in result)
                {
                    vehicle.DisplayDetails();
                }
            }
            else
            {
                Console.WriteLine("No vehicle found with this brand.");
            }
        }
        public int ReadInt()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
                Console.Write("Invalid number, try again:\t");
            return result;
        }
        public double ReadDouble()
        {
            double result;
            while (!double.TryParse(Console.ReadLine(), out result))
                Console.Write("Invalid figure, try again:\t");
            return result;
        }

        public (int Id, string Brand, string Model) ReadVehicleInformation()
        {
            Console.Write("Enter ID:\t");
            int id = ReadInt();

            Console.Write("Enter Brand:\t");
            string brand = Console.ReadLine();

            Console.Write("Enter Model:\t");
            string model = Console.ReadLine();

            return (id, brand, model);
        }

    }
}

