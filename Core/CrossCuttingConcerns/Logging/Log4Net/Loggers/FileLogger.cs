namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        //I sent the "JsonFileLogger" configuration that I prepared in the configuration file to base.
        public FileLogger() : base("JsonFileLogger")
        {
        }
    }
}
