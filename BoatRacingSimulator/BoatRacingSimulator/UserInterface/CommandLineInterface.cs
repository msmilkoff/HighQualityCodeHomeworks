namespace BoatRacingSimulator.UserInterface
{
    using System;
    using Interfaces;

    public class CommandLineInterface : IUserInterface
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string output)
        {
            Console.Write(output);
        }

        public void Write(params object[] args)
        {
            Console.Write(args);
        }

        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }

        public void WriteLine(params object[] args)
        {
            Console.WriteLine(args);
        }
    }
}
