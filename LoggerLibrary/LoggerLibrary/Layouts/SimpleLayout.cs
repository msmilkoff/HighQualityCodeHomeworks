namespace LoggerLibrary.Layouts
{
    using Contracts;

    public class SimpleLayout : ILayout
    {
        private const string SimpleFormat = "{0} - {1} - {2}";

        public SimpleLayout()
        {
            this.LogFormat = SimpleFormat;
        }

        public string LogFormat { get; }
    }
}
