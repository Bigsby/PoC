namespace SignalRing.Definitions
{
    public interface IHubClient
    {
        void Receive(string message);
    }
}
