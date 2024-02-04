using TomaszewskiWawrzyniak.MonitoryApp.Core;
using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrzyniak.MonitoryApp.DAOMOCK.BO
{
    public class Monitor : IMonitor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public float Diagonal { get; set; }
        public MatrixType Matrix { get; set; }
    }
}
