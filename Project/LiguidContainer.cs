namespace APBD_03.Project;

public class LiquidContainer : Container, IHazardNotifier
{
    public LiquidContainer(double maxPayload, string productType, double height, double depth, double width)
        : base(maxPayload, productType, height, depth, width) { }

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

            if (cargoWeight + MassOfCargo > maxAllowedWeight)
            {
                SendHazardNotification("Overload attempt detected!", SerialNumber);
                throw new OverfillException($"Cannot load {cargoWeight} kg. It exceeds the allowed weight for hazardous cargo.");
            }

            MassOfCargo += cargoWeight;
            Console.WriteLine($"Successfully loaded {cargoWeight} kg into liquid container. Mass of cargo: {MassOfCargo} kg.");
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