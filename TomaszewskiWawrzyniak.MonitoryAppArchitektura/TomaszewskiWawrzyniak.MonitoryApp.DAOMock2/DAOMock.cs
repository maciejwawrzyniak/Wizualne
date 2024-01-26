using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrzyniak.MonitoryApp.DAOMock2
{
    public class DAOMock : IDAO
    {
        private List<IProducer> producers;
        private List<IMonitor> monitors;

        public DAOMock()
        {
            producers = new List<IProducer>()
            {
                new BO.Producer() { ID = 1, Name = "ACER", Address = "Tajwan" },
                new BO.Producer() { ID = 2, Name = "IIYAMA", Address = "Japonia"},
                new BO.Producer() { ID = 3, Name = "HP", Address = "Stany zjednoczone"}
            };
            monitors = new List<IMonitor>()
            {
                new BO.Monitor() { ID = 1, Producer = producers[0], Name = "Nitro", Diagonal = 27.0f, Matrix = Core.MatrixType.VA  },
                new BO.Monitor() { ID = 2, Producer = producers[1], Name = "G-Master", Diagonal = 23.8f, Matrix = Core.MatrixType.IPS},
                new BO.Monitor() { ID = 3, Producer = producers[2], Name = "Omen", Diagonal = 27.0f, Matrix = Core.MatrixType.IPS},
                new BO.Monitor() { ID = 4, Producer = producers[0], Name = "Predator", Diagonal = 24.5f, Matrix = Core.MatrixType.IPS  }
            };
        }

        public IMonitor CreateNewMonitor()
        {
            return new BO.Monitor();
        }

        public IProducer CreateNewProducer()
        {
            return new BO.Producer();
        }

        public IEnumerable<IMonitor> GetAllMonitors()
        {
            return monitors;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }
    }
}
