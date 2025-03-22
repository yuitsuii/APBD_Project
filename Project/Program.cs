using APBD_03.Project;

class Program
{
    static void Main()
    {
        Console.WriteLine("\nCreating Gas Container...");
        GasContainer gasContainer = new GasContainer(500, "hazardous", 10, 250, 200, 150);
        gasContainer.LoadContainer(200);
        gasContainer.EmptyCargo();
        Console.WriteLine($"Gas Container Mass of Cargo after emptying: {gasContainer.MassOfCargo} kg\n");

        Console.WriteLine("Creating Liquid Container...");
        LiquidContainer liquidContainer = new LiquidContainer(1000, "hazardous", 250, 200, 150);
        liquidContainer.LoadContainer(400);
        Console.WriteLine($"Liquid Container Mass of Cargo: {liquidContainer.MassOfCargo} kg\n");

        Console.WriteLine("Creating Refrigerated Container with correct values...");
        RefrigeratedContainer refrigeratedContainer = new RefrigeratedContainer(800, "fish", 2, 250, 200, 150);
        Console.WriteLine("Refrigerated container created successfully.\n");

        Console.WriteLine("Creating Refrigerated Container with incorrect temperature...");
        try
        {
            RefrigeratedContainer refrigeratedContainer1 = new RefrigeratedContainer(800, "fish", 20, 250, 200, 150);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("\nAttempting to overload Liquid Container...");
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
        Ship ship2 = new Ship(30, 3, 4);

        Console.WriteLine("\nTransferring a container between ships...");
        ship.TransferContainer(ship2, refrigeratedContainer.SerialNumber);

        Console.WriteLine("\nFinal ship statuses:");
        Console.WriteLine("Ship 1:");
        ship.PrintShipInfo();
        Console.WriteLine("\nShip 2:");
        ship2.PrintShipInfo();

        GasContainer gasContainer2 = new GasContainer(500, "hazardous", 10, 25, 20, 150);
        gasContainer2.LoadContainer(200);
        ship2.LoadContainer(gasContainer2);

        Console.WriteLine("\nEdge Case 1: Loading a container beyond its maximum payload...");
        try
        {
            gasContainer.LoadContainer(600);
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }

        Console.WriteLine("\nEdge Case 2: Loading a hazardous liquid container beyond 50% capacity...");
        try
        {
            liquidContainer.LoadContainer(600);
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }

        Console.WriteLine("\nEdge Case 3: Loading a non-hazardous liquid container beyond 90% capacity...");
        LiquidContainer nonHazardousLiquidContainer = new LiquidContainer(1000, "milk", 250, 200, 150);
        try
        {
            nonHazardousLiquidContainer.LoadContainer(950);
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }

        Console.WriteLine("\nEdge Case 4: Loading a refrigerated container with an invalid product type...");
        try
        {
            RefrigeratedContainer invalidProductContainer = new RefrigeratedContainer(800, "invalid", 2, 250, 200, 150);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }

        Console.WriteLine("\nEdge Case 5: Loading a refrigerated container with a temperature outside the allowed range...");
        try
        {
            RefrigeratedContainer invalidTempContainer = new RefrigeratedContainer(800, "fish", 20, 250, 200, 150);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }

        Console.WriteLine("\nEdge Case 6: Transferring a container to a ship that is already at maximum capacity...");
        Ship fullShip = new Ship(25, 1, 2000);
        fullShip.LoadContainer(gasContainer);
        try
        {
            Console.WriteLine($"DEBUG: fullShip container count: {fullShip.Containers.Count}, Max allowed: {fullShip.MaxContainers}");
            ship2.TransferContainer(fullShip, gasContainer2.SerialNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }

        Console.WriteLine("\nEdge Case 7: Replacing a container with one that exceeds the ship's weight limit...");
        Container heavyContainer = new RefrigeratedContainer(5000, "fish", 2, 250, 200, 150);
        heavyContainer.LoadContainer(4500); 
        ship2.LoadContainer(heavyContainer);
        LiquidContainer liquidContainer2 = new LiquidContainer(1000, "hazardous", 25, 20, 150);
        ship2.LoadContainer(liquidContainer2);
        ship2.PrintShipInfo();
        try
        {
            Console.WriteLine("DEBUG: Containers before calling ReplaceContainer:");
            foreach (var c in ship2.Containers)
            {
                Console.WriteLine($" - {c.SerialNumber}");
            }
            ship2.ReplaceContainer(liquidContainer2.SerialNumber, heavyContainer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }

        Console.WriteLine("\nEdge Case 8: Unloading a container that is not on the ship...");
        try
        {
            ship.UnloadContainer("KON-C-999");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }

        Console.WriteLine("\nEdge Case 9: Removing a container that is not on the ship...");
        try
        {
            ship.RemoveContainer("KON-C-999");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }

        Console.WriteLine("\nEdge Case 10: Transferring a container that is not on the ship...");
        try
        {
            ship.TransferContainer(ship2, "KON-C-999");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }
    }
}
