namespace Logger
{
    public interface IFileLogger:ILogger
    {
        string Path { get; set; }
        int FileSize { get; set; }
    }
}
