using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels
{
    public partial class ProducerViewModel : ObservableObject, IProducer
    {
        [ObservableProperty]
        private Guid id;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? countryFrom;

        [ObservableProperty]
        private ICollection<IMonitor>? monitors;
        public ProducerViewModel(IProducer producer)
        {
            Id = producer.Id;
            Name = producer.Name;
            CountryFrom = producer.CountryFrom;
            Monitors = producer.Monitors;
        }
        public ProducerViewModel()
        {

        }

        public object Clone()
        {
            return new ProducerViewModel(this);
        }
    }
}
