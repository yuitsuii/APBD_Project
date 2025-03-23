namespace APBD_03.Project;

public abstract class Container
{
    private static int serialNumberCounter = 1;

    public double MassOfCargo { get; set; } 
    public double Height { get; set; }
    public double TareWeight { get; private set; }
    public double Depth { get; set; } 
    public double Width { get; set; } 
    public string SerialNumber { get; private set; }
    public double MaxPayload { get; set; } 
    public string ProductType { get; set; }
    public double Temperature { get; set; }
    
    private const double WeightPerCubicCentimeter = 0.000000001; 
    private const double WeightPerPayloadUnit = 0.00000001;

    protected Container(double maxPayload, string productType, double height, double depth, double width, double temperature = 0)
    {
        MaxPayload = maxPayload;
        ProductType = productType;
        Temperature = temperature;
        Height = height;
        Depth = depth;
        Width = width;
        SerialNumber = GenerateSerialNumber();
        TareWeight = CalculateTareWeight(); 
    }

    private string GenerateSerialNumber()
    {
        return $"KON-{GetContainerType()}-{serialNumberCounter++}";
    }

    private double CalculateTareWeight()
    {
        double volume = Width * Height * Depth;

        double tareWeight = (WeightPerCubicCentimeter * volume) + (WeightPerPayloadUnit * MaxPayload);

        return tareWeight;
    }

    public abstract string GetContainerType();

    public virtual void EmptyCargo()
    {
        MassOfCargo = 0;
        Console.WriteLine("Cargo has been emptied.");
    }

    public double GetMaxAllowedWeight()
    {
        if (ProductType == "hazardous")
        {
            return MaxPayload * 0.50;
        }
        return MaxPayload * 0.90;
    }

    public virtual void LoadContainer(double cargoWeight)
    {
        double allowedWeight = GetMaxAllowedWeight();
        if (MassOfCargo + cargoWeight > allowedWeight)
        {
            throw new OverfillException("Current load is too big.");
        }
        MassOfCargo += cargoWeight;
        Console.WriteLine("Loaded container successfully. Mass of cargo: " + MassOfCargo);
    }

    public void DisplayContainerInfo()
    {
        Console.WriteLine($"Container {SerialNumber} - Type: {GetContainerType()} - Mass of Cargo: {MassOfCargo} kg - Max Payload: {MaxPayload} kg - Tare Weight: {TareWeight} kg");
    }
}