namespace LoggerLibrary.Loggers
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Enums;

    public class Logger : ILogger
    {
        public Logger(params IAppender[] appenders)
        {
            this.Appenders = new List<IAppender>();
            AddAppenders(appenders);
        }

        public ICollection<IAppender> Appenders { get; }
        
        public void Info(string message)
        {
            Log(ReportLevel.Info, message, "Info");
        }

        public void Warn(string message)
        {
            Log(ReportLevel.Warning, message, "Warning");
        }

        public void Error(string message)
        {
            Log(ReportLevel.Error, message, "Error");
        }

        public void Critical(string message)
        {
            Log(ReportLevel.Critical, message, "Critical");
        }

        public void Fatal(string message)
        {
            Log(ReportLevel.Fatal, message, "Fatal");
        }

        private void AddAppenders(IAppender[] appenders)
        {
            foreach (IAppender appender in appenders)
            {
                this.Appenders.Add(appender);
            }
        }

        private void Log(ReportLevel reportLevel, string message, string logType)
        {
            foreach (var appender in this.Appenders.Where(appender => reportLevel >= appender.ReportLevel))
            {
                appender.Append(message, logType);
            }
        }
    }
}
