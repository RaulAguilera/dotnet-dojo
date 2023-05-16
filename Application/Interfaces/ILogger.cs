using Application.Enums;

namespace Application.Interfaces
{
    public interface ILogger<T>
    {
        public void Log(LogLevel level, string message, Exception? exception = null);
        public void LogTrace(string message);
        public void LogDebug(string message);
        public void LogInfo(string message);
        public void LogWarn(string message);
        public void LogError(string message, Exception exception);
        public void LogFatal(string message, Exception exception);
    }
}
