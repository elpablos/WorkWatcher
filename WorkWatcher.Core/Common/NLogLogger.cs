using NLog;
using System;

namespace Lorenzo.WorkWatcher.Core.Common
{
    /// <summary>
    /// Implementace knihovny nLog
    /// </summary>
    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetLogger("WorkWatcher");
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            _logger.Fatal(exception, message);
        }
    }
}
