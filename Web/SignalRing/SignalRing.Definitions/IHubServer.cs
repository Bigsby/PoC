namespace SignalRing.Definitions
{
    public interface IHubServer
    {
        void Send(string message);
    }
}
