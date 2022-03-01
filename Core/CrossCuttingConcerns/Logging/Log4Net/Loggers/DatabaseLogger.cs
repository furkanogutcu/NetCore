namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class DatabaseLogger : LoggerServiceBase
    {
        //I sent the "DatabaseLogger" configuration that I prepared in the configuration file to base.
        public DatabaseLogger() : base("DatabaseLogger")
        {
        }
    }
}
