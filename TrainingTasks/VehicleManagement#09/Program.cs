using VehicleManagement_09;

Console.WriteLine("Hello, World!");

VehicleManager manager = new VehicleManager();

while (true)
{
    Console.Clear();
    Console.WriteLine("\n===== Vehicle Management System =====");
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
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:

                Console.Write("Enter ID:\t");
                int carId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Brand:\t");
                string carBrand = Console.ReadLine();

                Console.Write("Enter Model:\t");
                string carModel = Console.ReadLine();

                Console.Write("Enter Number Of Doors:\t");
                int doors = Convert.ToInt32(Console.ReadLine());

                manager.AddVehicle(new Car(carId, carBrand, carModel, doors));

                break;

            case 2:

                Console.Write("Enter ID: ");
                int bikeId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Brand:\t");
                string bikeBrand = Console.ReadLine();

                Console.Write("Enter Model:\t");
                string bikeModel = Console.ReadLine();

                Console.Write("Has Gear (true/false):\t");
                bool hasGear = Convert.ToBoolean(Console.ReadLine());

                manager.AddVehicle(new Bike(bikeId, bikeBrand, bikeModel, hasGear));

                break;
            case 3:

                Console.Write("Enter ID:\t");
                int truckId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Brand:\t");
                string truckBrand = Console.ReadLine();

                Console.Write("Enter Model:\t");
                string truckModel = Console.ReadLine();

                Console.Write("Enter Load Capacity (Tons):\t");
                double capacity = Convert.ToDouble(Console.ReadLine());

                manager.AddVehicle(
                    new Truck(truckId, truckBrand, truckModel, capacity));

                break;
            case 4:

                Console.Write("Enter ID:\t");
                int busId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Brand:\t");
                string busBrand = Console.ReadLine();

                Console.Write("Enter Model:\t");
                string busModel = Console.ReadLine();

                Console.Write("Enter Seating Capacity:\t");
                int seats = Convert.ToInt32(Console.ReadLine());

                manager.AddVehicle(
                    new Bus(busId, busBrand, busModel, seats));

                break;

            case 5:
                Console.WriteLine("\nThe vehicles details are as follow\n");
                manager.DisplayVehicles();
                Console.WriteLine("\n****************** The vehicles details are completed here. ************************\n");
                break;
            case 6:

                Console.Write("Enter Vehicle ID: ");

                int id = Convert.ToInt32(Console.ReadLine());

                manager.SearchVehicleById(id);

                break;
            case 7:

                Console.Write("Enter Brand: ");

                string brand = Console.ReadLine();

                manager.SearchVehicleByBrand(brand);

                break;
            case 8:
                Console.WriteLine("The system has been closed successfully....");
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