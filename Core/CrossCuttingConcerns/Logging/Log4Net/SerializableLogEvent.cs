using System;
using log4net.Core;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
    public class SerializableLogEvent
    {
        private LoggingEvent _loggingEvent;

        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }

        public string UserName => _loggingEvent.UserName;
        public string Level => _loggingEvent.Level.Name;
        public DateTime DateTime => _loggingEvent.TimeStampUtc;
        public object MessageObject => _loggingEvent.MessageObject;
    }
}
