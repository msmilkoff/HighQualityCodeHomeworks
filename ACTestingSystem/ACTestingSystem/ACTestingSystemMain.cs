namespace ACTestingSystem
{
    using Core;
    using Core.Data;
    using Interfaces;
    using UI;

    public class ACTestingSystemMain
    {
        public static void Main() // BUG: Fix bug in test 5.
        {
            ITestingSystemDatabase database = new ACDatabase();
            IController controller = new Controller(database);
            ICommandManager commandManager = new CommandManager(controller);
            IUserInterface ui = new ConsoleUserInterface();

            IEngine engine = new Engine(ui, commandManager);
            engine.Run();
        }
    }
}