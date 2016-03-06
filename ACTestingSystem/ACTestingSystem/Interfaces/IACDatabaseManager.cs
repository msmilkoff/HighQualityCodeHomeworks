namespace ACTestingSystem.Interfaces
{
    public interface IACDatabaseManager
    {
        void AddAirConditioner(IAirConditioner airConditioner);

        IAirConditioner FindAirConditioner(string manufacturer, string model);

        int GetAirConditionersCount();
    }
}
