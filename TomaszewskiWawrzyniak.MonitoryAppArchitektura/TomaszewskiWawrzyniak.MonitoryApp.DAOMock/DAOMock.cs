using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrzyniak.MonitoryApp.DAOMock
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
                new BO.Producer() { ID = 2, Name = "ASUS", Address = "Tajwan"},
                new BO.Producer() { ID = 3, Name = "Samsung", Address = "Korea południowa"}
            };
            monitors = new List<IMonitor>()
            {
                new BO.Monitor() { ID = 1, Producer = producers[0], Name = "Nitro", Diagonal = 27.0f, Matrix = Core.MatrixType.VA  },
                new BO.Monitor() { ID = 2, Producer = producers[1], Name = "TUF Gaming", Diagonal = 23.8f, Matrix = Core.MatrixType.IPS},
                new BO.Monitor() { ID = 3, Producer = producers[2], Name = "Odyssey", Diagonal = 34.0f, Matrix = Core.MatrixType.VA},
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
