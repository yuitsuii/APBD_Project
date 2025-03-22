namespace APBD_03.Project;

public class RefrigeratedContainer : Container
{
    private static readonly Dictionary<string, (double MinTemp, double MaxTemp)> AllowedProducts = new()
    {
        { "bananas", (12.3, 14.3) },
        { "chocolate", (17, 19) },
        { "fish", (1, 3) },
        { "meat", (-16, -14) },
        { "ice cream", (-19, -18) },
        { "frozen pizza", (-31, -29) },
        { "cheese", (6.2, 8.2) },
        { "sausages", (4, 6) },
        { "butter", (19.5, 21.5) },
        { "eggs", (18, 20) }
    };

    public RefrigeratedContainer(double maxPayload, string productType, double temperature) 
        : base(maxPayload, productType, temperature) {
        try
        {
            if (!AllowedProducts.ContainsKey(productType))
            {
                throw new ArgumentException("Invalid product type for refrigerated container.");
            }

            var (minTemp, maxTemp) = AllowedProducts[productType];
            if (temperature < minTemp || temperature > maxTemp)
            {
                throw new ArgumentException($"Temperature out of range for {productType}. Allowed: {minTemp} - {maxTemp} °C");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error during container initialization: {ex.Message}");
        }
    }

    public override string GetContainerType()
    {
        return "C";
    }

    public override void LoadContainer(double cargoWeight)
    {
        try
        {
            double maxAllowedWeight = GetMaxAllowedWeight();
            
            if (CurrentLoad + cargoWeight > maxAllowedWeight)
            {
                throw new OverfillException($"Cannot load {cargoWeight} kg. It exceeds the allowed weight for hazardous cargo.");
            }
            
            CurrentLoad += cargoWeight;
            Console.WriteLine($"Successfully loaded {cargoWeight} kg into refrigerated container. Current load: {CurrentLoad} kg.");
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