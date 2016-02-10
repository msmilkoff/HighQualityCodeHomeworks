namespace LoggerLibrary.Appenders
{
    using Contracts;
    using Enums;
    using Layouts;

    public abstract class Appender : IAppender
    {
        protected Appender(ILayout layout)
        {
            this.Layout = layout;
        }

        public ILayout Layout { get; }

        public ReportLevel ReportLevel { get; set; }

        public abstract void Append(string message, string logType);
    }
}
