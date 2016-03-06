namespace ACTestingSystem.Interfaces
{
    using System.Collections.Generic;

    public interface IDatabase
    {
        IDictionary<string, IAirConditioner> AirConditioners { get; set; }

        IDictionary<string, IReport> Reports { get; set; }
    }
}
