using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaszewskiWawrzyniak.MonitoryApp.Core;
using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels
{
    public partial class MonitorViewModel : ObservableObject, IMonitor
    {
        [ObservableProperty]
        private Guid id;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private IProducer? producer;

        [ObservableProperty]
        private float diagonal;

        [ObservableProperty]
        private MatrixType matrix;

        public MonitorViewModel(IMonitor monitor)
        {
            Id = monitor.Id;
            Name = monitor.Name;
            Producer = monitor.Producer;
            Diagonal = monitor.Diagonal;
            Matrix = monitor.Matrix;
        }

        public MonitorViewModel()
        {
            Matrix = MatrixType.IPS;
        }

        public object Clone()
        {
            return new MonitorViewModel(this);
        }

        public IReadOnlyList<string> AllMatrixTypes { get; } = Enum.GetNames(typeof(MatrixType));
    }
}
