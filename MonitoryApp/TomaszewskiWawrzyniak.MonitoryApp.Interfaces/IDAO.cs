using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaszewskiWawrzyniak.MonitoryApp.Core;

namespace TomaszewskiWawrzyniak.MonitoryApp.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<IMonitor> GetAllMonitors();
        IProducer CreateNewProducer(Guid id, string name, string countryFrom);
        IMonitor CreateNewMonitor(Guid id, string name, IProducer producer, float diagonal, MatrixType matrixType);
        IProducer EditProducer(Guid id, string name, string countryFrom);
        IMonitor EditMonitor(Guid id, string name, IProducer producer, float diagonal, MatrixType matrixType);
        void DeleteProducer(Guid id);
        void DeleteMonitor(Guid id);
        IProducer? GetProducer(Guid id);
        IMonitor? GetMonitor(Guid id);

    }
}
