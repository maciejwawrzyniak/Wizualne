using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            Monitors = new ObservableCollection<MonitorViewModel>();
            foreach( var monitor in this.blc.GetMonitors() ) 
            {
                monitors.Add(new MonitorViewModel(monitor));
            }
            IsEditing = false;
            MonitorEdit = null;

            CancelCommand = new Command(
                execute: () =>
                {
                    MonitorEdit.PropertyChanged -= OnMonitorEditPropertyChanged;
                    MonitorEdit = null;
                    isEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return isEditing;
                });
        }

        [ObservableProperty]
        private MonitorViewModel monitorEdit;

        [ObservableProperty]
        private bool isEditing;
        [RelayCommand(CanExecute =nameof(CanCreateNewMonitor))]
        private void CreateNewMonitor()
        {
            MonitorEdit = new MonitorViewModel();
            MonitorEdit.PropertyChanged += OnMonitorEditPropertyChanged;
            IsEditing = true;

            RefreshCanExecute();    
        }

        private bool CanCreateNewMonitor()
        {
            return !IsEditing;
        }

        private void RefreshCanExecute()
        {
            CreateNewMonitorCommand.NotifyCanExecuteChanged();
            SaveMonitorCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command).ChangeCanExecute();
        }
        [RelayCommand(CanExecute = nameof(CanEditMonitorBeSaved))]
        private void SaveMonitor()
        {
            Monitors.Add(monitorEdit);
            MonitorEdit.PropertyChanged -= OnMonitorEditPropertyChanged;
            MonitorEdit = null;
            IsEditing = false;
            RefreshCanExecute ();
        }

        private bool CanEditMonitorBeSaved()
        {
            return MonitorEdit != null &&
                   MonitorEdit.Name != null &&
                   MonitorEdit.Name.Length > 1;
                   
        }

        private void OnMonitorEditPropertyChanged(object Sender, PropertyChangedEventArgs args)
        {
            SaveMonitorCommand.NotifyCanExecuteChanged();
        }

        public ICommand CancelCommand { get; set; }
    }
}
