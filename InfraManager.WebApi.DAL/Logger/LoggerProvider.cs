namespace InfraManager.WebApi.DAL.Logger
{
    using System;
    using System.IO;

    using Microsoft.Extensions.Logging;

    public class LoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        public void Dispose()
        {
        }

        private class MyLogger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(
                LogLevel logLevel,
                EventId eventId,
                TState state,
                Exception exception,
                Func<TState, Exception, string> formatter)
            {
                File.AppendAllText(
                    $"logs/logsql{DateTime.Now:yyyy-dd-MM}.txt",
                    $"{Environment.NewLine}{DateTime.Now:HH:mm:ss tt zz}\t{formatter(state, exception)}{Environment.NewLine}");
            }
        }
    }
}