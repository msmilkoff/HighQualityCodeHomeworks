namespace ACTestingSystem.Interfaces
{
    public interface ICommand
    {
        string Name { get; set; }

        string[] Parameters { get; set; }
    }
}