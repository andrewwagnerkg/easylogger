namespace Logger
{
    public interface ILogger
    {
        void Write<T>(string text) where T : class;
    }
}
