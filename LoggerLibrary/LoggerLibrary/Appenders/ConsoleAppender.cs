namespace LoggerLibrary.Appenders
{
    using System;
    using Contracts;
    using Layouts;

    public class ConsoleAppender : Appender
    {
        public ConsoleAppender(ILayout layout) : base(layout)
        {
        }

        public override void Append(string message, string logType)
        {
            var date = DateTime.Now;

            Console.WriteLine(this.Layout.LogFormat, date, logType, message);
        }
    }
}
