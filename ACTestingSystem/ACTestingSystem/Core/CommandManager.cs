namespace ACTestingSystem.Core
{
    using System;
    using Interfaces;
    using Models.Enums;
    using Utilities;

    public class CommandManager : ICommandManager
    {
        public CommandManager(IController controller)
        {
            this.Controller = controller;
        }

        private IController Controller { get; }

        public string Execute(ICommand command)
        {
            string output;
            
            switch (command.Name)
            {
                case "RegisterStationaryAirConditioner":
                    this.ValidateParametersCount(command, 4);
                    EfficiancyRating energyEfficiencyRating;
                    try
                    {
                        energyEfficiencyRating =
                            (EfficiancyRating)Enum.Parse(typeof(EfficiancyRating), command.Parameters[2]);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException(Messages.IncorrectRating);
                    }

                    output = this.Controller.RegisterStationaryAirConditioner(
                        command.Parameters[0],
                        command.Parameters[1],
                        energyEfficiencyRating,
                        int.Parse(command.Parameters[3]));
                    break;
                case "RegisterCarAirConditioner":
                    this.ValidateParametersCount(command, 3);
                    string manufacturer = command.Parameters[0];
                    string model = command.Parameters[1];
                    int volumeCoverage = int.Parse(command.Parameters[2]);
                    output = this.Controller.RegisterCarAirConditioner(manufacturer, model, volumeCoverage);
                    break;
                case "RegisterPlaneAirConditioner":
                    this.ValidateParametersCount(command, 4);
                    output = this.Controller.RegisterPlaneAirConditioner(
                        command.Parameters[0],
                        command.Parameters[1],
                        int.Parse(command.Parameters[2]),
                        int.Parse(command.Parameters[3]));
                    break;
                case "TestAirConditioner":
                    this.ValidateParametersCount(command, 2);
                    output = this.Controller.TestAirConditioner(command.Parameters[0], command.Parameters[1]);
                    break;
                case "FindAirConditioner":
                    this.ValidateParametersCount(command, 2);
                    output = this.Controller.FindAirConditioner(command.Parameters[0], command.Parameters[1]);
                    break;
                case "FindReport":
                    this.ValidateParametersCount(command, 2);
                    output = this.Controller.FindReport(command.Parameters[0], command.Parameters[1]);
                    break;
                case "FindAllReportsByManufacturer":
                    this.ValidateParametersCount(command, 1);
                    output = this.Controller.FindAllReportsByManufacturer(command.Parameters[0]);
                    break;
                case "Status":
                    this.ValidateParametersCount(command, 0);
                    output = this.Controller.Status();
                    break;
                default:
                    throw new InvalidOperationException(Messages.InvalidCommand);
            }

            return output;
        }

        private void ValidateParametersCount(ICommand input, int count)
        {
            if (input.Parameters.Length != count)
            {
                throw new InvalidOperationException(Messages.InvalidCommand);
            }
        }
    }
}
