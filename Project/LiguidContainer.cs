namespace APBD_03.Project;

public class LiquidContainer : Container, IHazardNotifier
{
    public LiquidContainer(double maxPayload, string productType) : base(maxPayload, productType) { }

    public override string GetContainerType()
    {
        return "L";
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
            Console.WriteLine($"Successfully loaded {cargoWeight} kg into liquid container. Current load: {CurrentLoad} kg.");
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