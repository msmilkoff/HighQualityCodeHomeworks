namespace LoggerLibrary.Contracts
{
    using Enums;

    public interface IAppender
    {
        ILayout Layout { get; }

        ReportLevel ReportLevel { get; set; }

        void Append(string message, string logType);
    }
}
