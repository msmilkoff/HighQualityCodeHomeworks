namespace ACTestingSystem.Core
{
    using System;
    using Interfaces;

    public class Engine : IEngine
    {
        private readonly IUserInterface ui;
        private readonly ICommandManager commandManager;

        public Engine(IUserInterface userInterface, ICommandManager commandManager)
        {
            this.ui = userInterface;
            this.commandManager = commandManager;
        }

        public void Run()
        {
            while (true)
            {
                string input = this.ui.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                input = input.Trim();

                string output;
                try
                {
                    var command = new Command(input);
                    output = this.commandManager.Execute(command);
                }
                catch (Exception ex)
                {
                    output = ex.Message;
                }

                this.ui.WriteLine(output);
            }
        }
    }
}