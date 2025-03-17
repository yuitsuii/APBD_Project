namespace APBD_03.Project;

public class Ship
{
    public double MaxSpeed { get; private set; }
    public int MaxContainers { get; private set; }
    public double MaxWeight { get; private set; } 
    public List<Container> Containers { get; private set; }

    public Ship(double maxSpeed, int maxContainers, double maxWeight)
    {
        MaxSpeed = maxSpeed;
        MaxContainers = maxContainers;
        MaxWeight = maxWeight * 1000; // Convert to kg
        Containers = new List<Container>();
    }

    public void LoadContainer(Container container)
    {
        if (Containers.Count >= MaxContainers)
        {
            Console.WriteLine("Error: Ship cannot carry more containers.");
            return;
        }

        if (GetTotalWeight() + (container.TareWeight + container.CurrentLoad) > MaxWeight)
        {
            Console.WriteLine("Error: Ship's weight capacity exceeded.");
            return;
        }

        Containers.Add(container);
        Console.WriteLine($"Container {container.SerialNumber} loaded onto the ship.");
    }

    public void LoadContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainer(container);
        }
    }

    public void RemoveContainer(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            Console.WriteLine($"Error: Container {serialNumber} not found.");
            return;
        }

        Containers.Remove(container);
        Console.WriteLine($"Container {serialNumber} removed from the ship.");
    }

    public void UnloadContainer(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            Console.WriteLine($"Error: Container {serialNumber} not found.");
            return;
        }

        container.EmptyCargo();
        Console.WriteLine($"Container {serialNumber} has been unloaded.");
    }

    public void ReplaceContainer(string oldSerial, Container newContainer)
    {
        if (!Containers.Any(c => c.SerialNumber == oldSerial))
        {
            Console.WriteLine($"Error: Container {oldSerial} not found.");
            return;
        }
        
        RemoveContainer(oldSerial);
        LoadContainer(newContainer);
        Console.WriteLine($"Container {oldSerial} replaced with {newContainer.SerialNumber}.");
    }

    public void TransferContainer(Ship otherShip, string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            Console.WriteLine($"Error: Container {serialNumber} not found.");
            return;
        }

        RemoveContainer(serialNumber);
        otherShip.LoadContainer(container);
        Console.WriteLine($"Container {serialNumber} transferred to another ship.");
    }

    public void PrintShipInfo()
    {
        Console.WriteLine($"Ship Info - Max Speed: {MaxSpeed} knots, Max Containers: {MaxContainers}, Max Weight: {MaxWeight / 1000} tons");
        Console.WriteLine($"Current Containers: {Containers.Count}");
        foreach (var container in Containers)
        {
            container.DisplayContainerInfo();
        }
    }

    private double GetTotalWeight()
    {
        return Containers.Sum(c => c.TareWeight + c.CurrentLoad);
    }
}
