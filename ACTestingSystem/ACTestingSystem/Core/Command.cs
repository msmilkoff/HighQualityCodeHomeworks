namespace ACTestingSystem.Core
{
    using System;
    using Interfaces;

    public class Command : ICommand
    {
        public Command(string input)
        {
            this.InitCommand(input);
        }

        public string Name { get; set; }

        public string[] Parameters { get; set; }

        private void InitCommand(string input)
        {
            this.Name = input.Substring(0, input.IndexOf(' '));

            int startIndex = input.IndexOf(' ') + 1;
            char[] separators = { '(', ')', ',' };
            this.Parameters = input.Substring(startIndex).Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}