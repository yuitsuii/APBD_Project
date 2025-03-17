using APBD_03.Project;

class Program
{
    static void Main()
    {
        Console.WriteLine("Creating Gas Container...");
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

        Console.WriteLine("\nCreating Refrigerated Container with incorrect temperature...");
        RefrigeratedContainer refrigeratedContainer1 = new RefrigeratedContainer(800, "fish", 20);

        
        
        
        Console.WriteLine("Attempting to overload Liquid Container...");
            
        liquidContainer.LoadContainer(700);
            
           

    }
}