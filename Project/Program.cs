using APBD_03.Project;

class Program
{
    static void Main()
    {
        Console.WriteLine("\nCreating Gas Container...");
        GasContainer gasContainer = new GasContainer(500, "hazardous", 10);
        gasContainer.LoadContainer(200);
        gasContainer.EmptyCargo();
        Console.WriteLine($"Gas Container Load after emptying: {gasContainer.CurrentLoad} kg\n");

        Console.WriteLine("Creating Liquid Container...");
        LiquidContainer liquidContainer = new LiquidContainer(1000, "hazardous");
        liquidContainer.LoadContainer(400);
        Console.WriteLine($"Liquid Container Load: {liquidContainer.CurrentLoad} kg\n");

        Console.WriteLine("Creating Refrigerated Container with correct values...");
        RefrigeratedContainer refrigeratedContainer = new RefrigeratedContainer(800, "fish", 2);
        Console.WriteLine("Refrigerated container created successfully.\n");

        Console.WriteLine("Creating Refrigerated Container with incorrect temperature...");
        RefrigeratedContainer refrigeratedContainer1 = new RefrigeratedContainer(800, "fish", 20);

        Console.WriteLine("Attempting to overload Liquid Container...");
        liquidContainer.LoadContainer(700);


        
        Console.WriteLine("\nCreating a Ship...");
        Ship ship = new Ship(25, 2, 2000); 
        
        Console.WriteLine("\nLoading containers onto the ship...");
        ship.LoadContainer(gasContainer);
        ship.LoadContainer(liquidContainer);

        Console.WriteLine("\nAttempting to overload the ship with another container...");
        ship.LoadContainer(refrigeratedContainer);

        Console.WriteLine("\nShip Information:");
        ship.PrintShipInfo();

        Console.WriteLine("\nRemoving a container...");
        ship.RemoveContainer(gasContainer.SerialNumber);
        ship.PrintShipInfo();

        Console.WriteLine("\nUnloading a container...");
        ship.UnloadContainer(liquidContainer.SerialNumber);
        ship.PrintShipInfo();

        Console.WriteLine("\nReplacing a container...");
        ship.ReplaceContainer(liquidContainer.SerialNumber, refrigeratedContainer);
        ship.PrintShipInfo();

        Console.WriteLine("\nCreating another ship for transfer testing...");
        Ship ship2 = new Ship(30, 3, 3000);
        
        Console.WriteLine("\nTransferring a container between ships...");
        ship.TransferContainer(ship2, refrigeratedContainer.SerialNumber);
        
        Console.WriteLine("\nFinal ship statuses:");
        Console.WriteLine("Ship 1:");
        ship.PrintShipInfo();
        Console.WriteLine("\nShip 2:");
        ship2.PrintShipInfo();
    }
}