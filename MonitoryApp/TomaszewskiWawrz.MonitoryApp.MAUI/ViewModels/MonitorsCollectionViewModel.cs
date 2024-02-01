using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TomaszewskiWawrzyniak.MonitoryApp.BLC;

namespace TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels
{
    public partial class MonitorsCollectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MonitorViewModel> monitors;

        private BLC blc;

        public MonitorsCollectionViewModel(BLC blc)
        {
            this.blc = blc;
            monitors = new ObservableCollection<MonitorViewModel>();

            /* Unmerged change from project 'TomaszewskiWawrz.MonitoryApp.MAUI (net8.0-maccatalyst)'
            Before:
                        foreach( var monitor in this.blc.GetMonitors() ) 
            After:
                        foreach (var monitor in this.blc.GetMonitors() ) 
            */

            /* Unmerged change from project 'TomaszewskiWawrz.MonitoryApp.MAUI (net8.0-windows10.0.19041.0)'
            Before:
                        foreach( var monitor in this.blc.GetMonitors() ) 
            After:
                        foreach (var monitor in this.blc.GetMonitors() ) 
            */

            /* Unmerged change from project 'TomaszewskiWawrz.MonitoryApp.MAUI (net8.0-ios)'
            Before:
                        foreach( var monitor in this.blc.GetMonitors() ) 
            After:
                        foreach (var monitor in this.blc.GetMonitors() ) 
            */
            foreach (var monitor in this.blc.GetMonitors())
            {
                monitors.Add(new MonitorViewModel(monitor));
            }
        }
    }
}
