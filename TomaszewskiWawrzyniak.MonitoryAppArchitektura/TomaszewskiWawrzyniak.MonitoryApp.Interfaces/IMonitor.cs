using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaszewskiWawrzyniak.MonitoryApp.Core;

namespace TomaszewskiWawrzyniak.MonitoryApp.Interfaces
{
    public interface IMonitor
    {
        int ID { get; set; }
        string Name { get; set; }
        IProducer Producer { get; set; }
        float Diagonal {  get; set; }
        MatrixType Matrix { get; set; }

    }
}
