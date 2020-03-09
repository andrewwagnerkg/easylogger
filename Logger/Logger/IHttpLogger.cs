namespace Logger
{
    public interface IHttpLogger:ILogger
    {
        string Url { get; set; }
        bool IsPOST { get; set; }
    }
}
