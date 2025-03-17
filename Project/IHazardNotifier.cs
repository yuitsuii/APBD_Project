namespace APBD_03.Project;

public interface IHazardNotifier
{
    void SendHazardNotification(string message, string containerNumber);
}