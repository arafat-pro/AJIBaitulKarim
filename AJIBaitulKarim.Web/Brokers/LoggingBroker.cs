using Microsoft.Extensions.Logging;

namespace AJIBaitulKarim.Web.Brokers
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger logger;
        // Recommended Single Liner Declaration to be Fat Arrow Implemented for Code Cleaner
        public LoggingBroker(ILogger logger) => this.logger = logger;
        public void Error(string message) => this.logger.LogError(message);

        //public LoggingBroker(ILogger logger)
        //{
        //    this.logger = logger;
        //}

        //public void Error(string message)
        //{
        //    this.logger.LogError(message);
        //}
    }
}