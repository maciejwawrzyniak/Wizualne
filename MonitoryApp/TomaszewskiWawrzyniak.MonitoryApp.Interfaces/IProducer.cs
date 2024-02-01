namespace TomaszewskiWawrzyniak.MonitoryApp.Interfaces
{
    public interface IProducer
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string CountryFrom { get; set; }
        ICollection<IMonitor> Monitors { get; set; }
    }
}
