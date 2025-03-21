namespace APBD_03.Project;

public abstract class Container
{
    private static int serialNumberCounter = 1;

    public double MassOfCargo { get; set; }
    public double Height { get; set; }
    public double TareWeight { get; set; }
    public double Depth { get; set; }
    public string SerialNumber { get; private set; }
    public double MaxPayload { get; set; }
    public double CurrentLoad { get; set; }
    public string ProductType { get; set; }  
    public double Temperature { get; set; }
    
    
    protected Container(double maxPayload, string productType, double temperature = 0)
    {
        MaxPayload = maxPayload;
        ProductType = productType;
        Temperature = temperature;
        SerialNumber = GenerateSerialNumber();
    }
    
    private string GenerateSerialNumber()
    {
        return $"KON-{GetContainerType()}-{serialNumberCounter++}";
    }
    
    public abstract string GetContainerType();
    
    public virtual void EmptyCargo()
    {
        CurrentLoad = 0;
        Console.WriteLine("Cargo has been emptied.");
    }

    public double GetMaxAllowedWeight() {
        if (ProductType == "hazardous")  
        {
            return MaxPayload * 0.50;
        }
        return MaxPayload * 0.90;  
    }

    public virtual void LoadContainer(double cargoWeight)
    {
        double allowedWeight = GetMaxAllowedWeight();
        if (CurrentLoad + cargoWeight >allowedWeight)
        {
            throw new OverflowException("Current load is too big.");
        }
        CurrentLoad += cargoWeight;
        Console.WriteLine("Loaded container successfully. Current load: " + CurrentLoad);
    }
    
    public void DisplayContainerInfo()
    {
        Console.WriteLine($"Container {SerialNumber} - Type: {GetContainerType()} - Current Load: {CurrentLoad} kg - Max Payload: {MaxPayload} kg");
    }
    
}