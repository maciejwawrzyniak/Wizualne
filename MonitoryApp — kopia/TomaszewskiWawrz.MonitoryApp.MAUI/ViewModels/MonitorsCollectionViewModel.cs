using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaszewskiWawrzyniak.MonitoryApp.BLC;
using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels
{
    public partial class MonitorsCollectionViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MonitorViewModel> monitors;

        private BLC blc;

        public MonitorsCollectionViewModel( BLC blc)
        {
            this.blc = blc;
            monitors = new ObservableCollection<MonitorViewModel>();
            foreach( var monitor in this.blc.GetMonitors() ) 
            {
                monitors.Add(new MonitorViewModel(monitor));
            }
        }
    }
}
