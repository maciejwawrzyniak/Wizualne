namespace TomaszewskiWawrzyniak.MonitoryApp.Interfaces
{
    public interface IProducer
    {
        Guid ID { get; set; }
        string Name { get; set; }
        string CountryFrom { get; set; }
    }
}
