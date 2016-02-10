namespace LoggerLibrary.Appenders
{
    using System;
    using System.IO;
    using Contracts;
    using Layouts;

    public class FileAppender : Appender
    {
        private string file;
        
        public FileAppender(ILayout layout) : base(layout)
        {
        }

        public string File
        {
            get { return this.file; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new FileNotFoundException($"File {value} not found", nameof(value));
                }

                this.file = value;
            }
        }

        public override void Append(string message, string logType)
        {
            using (var writer = new StreamWriter(this.File))
            {
                writer.WriteLine(this.Layout.LogFormat, DateTime.Now, logType, message);
            }
        }
    }
}