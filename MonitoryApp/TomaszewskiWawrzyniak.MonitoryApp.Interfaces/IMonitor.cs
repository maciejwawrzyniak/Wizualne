using TomaszewskiWawrzyniak.MonitoryApp.Core;

namespace TomaszewskiWawrzyniak.MonitoryApp.Interfaces
{
    public interface IMonitor
    {
        Guid Id { get; set; }
        string Name { get; set; }
        IProducer Producer { get; set; }
        float Diagonal { get; set; }
        MatrixType Matrix { get; set; }

    }
}
