using UtilityLibrary;
using VehicleManagement_09;

VehicleManager manager = new VehicleManager();

while (true)
{
    Console.Clear();
    Console.WriteLine("\n===== Vehicle Management System =====");
    Loggings.MessageLog("===== Vehicle Management System =====");
    Console.WriteLine("1. Add Car");
    Console.WriteLine("2. Add Bike");
    Console.WriteLine("3. Add Truck");
    Console.WriteLine("4. Add Bus");
    Console.WriteLine("5. Display All Vehicles");
    Console.WriteLine("6. Search Vehicle By ID");
    Console.WriteLine("7. Search Vehicle By Brand");
    Console.WriteLine("8. Exit");

    Console.Write("Enter Choice : ");

    try
    {
        int choice = manager.ReadInt();

        switch (choice)
        {
            case 1:

                var carDetails = manager.ReadVehicleInformation();

                Console.Write("Enter Number Of Doors:\t");
                int doors = manager.ReadInt();

                manager.AddVehicle(new Car(carDetails.Id, carDetails.Brand, carDetails.Model, doors));
                Loggings.MessageLog("Car has addedd.");

                break;

            case 2:

                var bikeDetails = manager.ReadVehicleInformation();


                Console.Write("Has Gear (true/false):\t");
                bool hasGear = Convert.ToBoolean(Console.ReadLine());

                manager.AddVehicle(new Bike(bikeDetails.Id, bikeDetails.Brand, bikeDetails.Model, hasGear));
                Loggings.MessageLog("Bike has addedd.");
                break;
            case 3:

               var truckDetails=manager.ReadVehicleInformation();

                Console.Write("Enter Load Capacity (Tons):\t");
                double capacity = manager.ReadDouble();

                manager.AddVehicle(new Truck(truckDetails.Id, truckDetails.Brand, truckDetails.Model, capacity));
                Loggings.MessageLog("Truck has addedd.");

                break;
            case 4:

                var busDetails=manager.ReadVehicleInformation();

                Console.Write("Enter Seating Capacity:\t");
                int seats = manager.ReadInt();

                manager.AddVehicle(new Bus(busDetails.Id, busDetails.Brand, busDetails.Model, seats));

                break;

            case 5:
                Console.WriteLine("\nThe vehicles details are as follow\n");
                manager.DisplayVehicles();
                Console.WriteLine("\n****************** The vehicles details are completed here. ************************\n");
                break;
            case 6:

                Console.Write("Enter Vehicle ID: ");

                int id = manager.ReadInt();

                manager.SearchVehicleById(id);

                break;
            case 7:

                Console.Write("Enter Brand: ");

                string brand = Console.ReadLine();

                manager.SearchVehicleByBrand(brand);

                break;
            case 8:
                Console.WriteLine("The system has been closed successfully....");
                Loggings.MessageLog("APP TERMINATED");
                Console.ReadKey();
                return;

            default:

                Console.WriteLine("\nInvalid Menu Choice.");
                continue;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input! Please enter correct data.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error : {ex.Message}");
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}