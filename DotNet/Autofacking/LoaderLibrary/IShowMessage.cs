namespace LoaderLibrary
{
    public interface IShowMessage
    {
        void ShowMessage();
    }

    public interface IMessageProvider
    {
        string Message { get; }
    }
}