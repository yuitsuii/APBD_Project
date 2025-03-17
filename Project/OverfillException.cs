namespace APBD_03.Project;

public class OverfillException : Exception
{
    public OverfillException(string message) : base(message) { }
}