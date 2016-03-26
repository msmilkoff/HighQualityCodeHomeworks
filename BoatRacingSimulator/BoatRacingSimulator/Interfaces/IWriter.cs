namespace BoatRacingSimulator.Interfaces
{
    public interface IWriter
    {
        void Write(string output);

        void Write(params object[] args);

        void WriteLine(string output);

        void WriteLine(params object[] args);
    }
}
