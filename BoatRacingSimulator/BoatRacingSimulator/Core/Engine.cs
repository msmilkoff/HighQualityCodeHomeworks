namespace BoatRacingSimulator.Core
{
    using System;
    using System.Linq;
    using BoatRacingSimulator.Interfaces;
    using BoatRacingSimulator.UserInterface;

    public class Engine : IEngine
    {
        private readonly IUserInterface userInterface;

        public Engine(ICommandHandler commandHandler, IUserInterface userInterface)
        {
            this.CommandHandler = commandHandler;
            this.userInterface = userInterface;
        }

        public Engine() : this(new CommandHandler(), new CommandLineInterface())
        {
        }

        public ICommandHandler CommandHandler { get; }

        public void Run()
        {
            while (true)
            {
                string line = this.userInterface.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                var tokens = line.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                var commandName = tokens[0];
                var parameters = tokens.Skip(1).ToArray();

                try
                {
                    string commandResult = this.CommandHandler.ExecuteCommand(commandName, parameters);
                    this.userInterface.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    this.userInterface.WriteLine(ex.Message);
                }
            }
        }
    }
}