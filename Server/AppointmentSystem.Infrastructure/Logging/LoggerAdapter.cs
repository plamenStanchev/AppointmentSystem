namespace AppointmentSystem.Infrastructure.Logging
{
    using AppointmentSystem.Core.Interfaces.Infrastructure;
    using Microsoft.Extensions.Logging;

    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> logger;
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            this.logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            this.logger.LogInformation(message, args);
        }
    }
}
