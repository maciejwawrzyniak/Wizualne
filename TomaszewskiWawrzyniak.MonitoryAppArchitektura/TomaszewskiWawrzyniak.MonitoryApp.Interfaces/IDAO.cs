using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaszewskiWawrzyniak.MonitoryApp.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<IMonitor> GetAllMonitors();
        IProducer CreateNewProducer();
        IMonitor CreateNewMonitor();

    }
}
