using TomaszewskiWawrzyniak.MonitoryApp.Core;

namespace TomaszewskiWawrzyniak.MonitoryApp.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<IMonitor> GetAllMonitors();
        IProducer CreateNewProducer(string name, string countryFrom);
        IMonitor CreateNewMonitor(string name, Guid producer, float diagonal, MatrixType matrixType);
        IProducer EditProducer(Guid id, string name, string countryFrom);
        IMonitor EditMonitor(Guid id, string name, Guid producer, float diagonal, MatrixType matrixType);
        void DeleteProducer(Guid id);
        void DeleteMonitor(Guid id);
        IProducer? GetProducer(Guid id);
        IMonitor? GetMonitor(Guid id);

    }
}
