namespace APBD_03.Project;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; set; }

    public GasContainer(double maxPayload, string productType, double pressure) : base(maxPayload, productType)
    {
        Pressure = pressure;
    }

    public override string GetContainerType()
    {
        return "G";
    }

    public override void EmptyCargo()
    {
        try
        {
            CurrentLoad = CurrentLoad * 0.05;
            Console.WriteLine($"Gas container emptied. 5% of the cargo remains: {CurrentLoad} kg.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while emptying cargo: {ex.Message}");
        }
    }

    public void SendHazardNotification(string message, string containerNumber)
    {
        Console.WriteLine($"[Hazard Notification] Container {containerNumber}: {message}");
    }

    public override void LoadContainer(double cargoWeight)
    {
        try
        {
            double maxAllowedWeight = GetMaxAllowedWeight();

            if (cargoWeight + CurrentLoad > maxAllowedWeight)
            {
                SendHazardNotification("Overload attempt detected!", SerialNumber);
                throw new OverfillException($"Cannot load {cargoWeight} kg. It exceeds the allowed weight for hazardous cargo.");
            }

            CurrentLoad += cargoWeight;
            Console.WriteLine($"Successfully loaded {cargoWeight} kg into gas container. Current load: {CurrentLoad} kg.");
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"Warning: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while loading cargo: {ex.Message}");
        }
    }
}