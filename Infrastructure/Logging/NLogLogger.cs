using Application.Interfaces;
using NLog;

namespace Infrastructure.Logging
{
    public class NLogLogger<T> : ILogger<T>
    {
        private readonly Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetLogger(typeof(T).Name);
        }

        public void Log(Application.Enums.LogLevel level, string message, Exception? exception = null)
        {
            switch (level)
            {
                case Application.Enums.LogLevel.Trace:
                    LogTrace(message);
                    break;
                case Application.Enums.LogLevel.Debug:
                    LogDebug(message);
                    break;
                case Application.Enums.LogLevel.Info:
                    LogInfo(message);
                    break;
                case Application.Enums.LogLevel.Warn:
                    LogWarn(message);
                    break;
                case Application.Enums.LogLevel.Error:
                    LogError(message, exception);
                    break;
                case Application.Enums.LogLevel.Fatal:
                    LogFatal(message, exception);
                    break;
                default:
                    LogDebug(message);
                    break;
            }
        }

        public void LogTrace(string message)
        {
            _logger.Trace(message);
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message) 
        {
            _logger.Warn(message);
        }

        public void LogError(string message, Exception ex) 
        {
            _logger.Error(ex, message, new object[] { });
        }

        public void LogFatal(string message, Exception ex) 
        {
            _logger.Fatal(ex, message);
        }
    }
}
