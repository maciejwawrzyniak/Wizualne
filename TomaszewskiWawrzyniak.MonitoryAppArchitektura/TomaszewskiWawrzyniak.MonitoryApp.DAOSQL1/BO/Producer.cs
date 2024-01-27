using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1.BO
{
    public class Producer : IProducer
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string CountryFrom { get; set; }
        public ICollection<IMonitor> Monitors {  get; set; }
    }
}
