namespace ChatWpf.Core.Logging.Core
{
    public interface ILogger
    {
        void Log(string message, LogLevel level);
    }
}