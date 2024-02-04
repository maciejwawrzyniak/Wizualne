using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrzyniak.MonitoryApp.DAOMOCK.BO
{
    public class Producer : IProducer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryFrom { get; set; }
        public ICollection<IMonitor> Monitors { get; set; }
    }
}
