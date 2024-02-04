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
using TomaszewskiWawrzyniak.MonitoryApp.Core;

namespace TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels
{
    public partial class MonitorsCollectionViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MonitorViewModel> monitors;

        [ObservableProperty]
        private ObservableCollection<IProducer> producers;
        public ObservableCollection<IProducer> GetProducers
        {
            get {
                    return producers; 
                }
            set
            {
                if (value != producers)
                {
                    producers = value;
                    OnPropertyChanged(nameof(producers));
                }
            }
        }

        [ObservableProperty]
        private string selectedMatrix;
        public string GetSelectedMatrix
        {
            get { return selectedMatrix; }
            set
            {
                if (SetProperty(ref selectedMatrix, value))
                {
                    if (MonitorEdit != null)
                    {
                        MonitorEdit.Matrix = (MatrixType)Enum.Parse(typeof(MatrixType), selectedMatrix);
                    }
                }

            }
        }
        [ObservableProperty]
        private string selectedMatrixFilter;
        public string GetSelectedMatrixFilter
        {
            get { return selectedMatrixFilter; }
            set
            {
                if (SetProperty(ref selectedMatrixFilter, value))
                {
                    if (MonitorFilter != null)
                    {
                        MonitorFilter.Matrix = (MatrixType)Enum.Parse(typeof(MatrixType), selectedMatrixFilter);
                    }
                }

            }
        }

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
            IsCreating = false;
            MonitorEdit = null;
            MonitorFilter = new MonitorViewModel();

            CancelCommand = new Command(
                execute: () =>
                {
                    MonitorEdit.PropertyChanged -= OnMonitorEditPropertyChanged;
                    MonitorEdit = null;
                    IsCreating = false;
                    IsEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return isEditing || isCreating;
                });
            RefreshProducers();
        }

        [ObservableProperty]
        private MonitorViewModel monitorEdit;
        [ObservableProperty]
        private MonitorViewModel monitorFilter;


        [ObservableProperty]
        private bool isCreating;
        [ObservableProperty]
        private bool isEditing;
        [RelayCommand(CanExecute =nameof(CanCreateNewMonitor))]
        private void CreateNewMonitor()
        {
            MonitorEdit = new MonitorViewModel();
            MonitorEdit.PropertyChanged += OnMonitorEditPropertyChanged;
            IsCreating = true;
            IsEditing = false;
            RefreshCanExecute();    
        }

        private bool CanCreateNewMonitor()
        {
            return !IsCreating;
        }

        private bool CanEditNewMonitor()
        {
            return !IsEditing;
        }

        private void RefreshCanExecute()
        {
            CreateNewMonitorCommand.NotifyCanExecuteChanged();
            SaveMonitorCommand.NotifyCanExecuteChanged();
            EditMonitorCommand.NotifyCanExecuteChanged();
            DeleteMonitorCommand.NotifyCanExecuteChanged();
            FilterMonitorsCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command).ChangeCanExecute();
        }
        [RelayCommand(CanExecute = nameof(CanEditMonitorBeSaved))]
        private void SaveMonitor()
        {
            if(IsCreating)
            {
                blc.CreateNewMonitor(MonitorEdit.Name, MonitorEdit.Producer.Id, MonitorEdit.Diagonal, MonitorEdit.Matrix);
            }
            else
            {
                blc.EditMonitor(MonitorEdit.Id, MonitorEdit.Name, MonitorEdit.Producer.Id, MonitorEdit.Diagonal, MonitorEdit.Matrix);
            }
            //Monitors.Add(monitorEdit);
            MonitorEdit.PropertyChanged -= OnMonitorEditPropertyChanged;
            MonitorEdit = null;
            isCreating = false;
            IsEditing = false;
            RefreshCanExecute ();
            RefreshMonitors();
        }

        private bool CanEditMonitorBeSaved()
        {
            return MonitorEdit != null &&
                   MonitorEdit.Name != null &&
                   MonitorEdit.Name.Length > 1 &&
                   MonitorEdit.Diagonal > 0 &&
                   MonitorEdit.Producer != null
                ;
                   
        }

        private void OnMonitorEditPropertyChanged(object Sender, PropertyChangedEventArgs args)
        {
            SaveMonitorCommand.NotifyCanExecuteChanged();
            CreateNewMonitorCommand.NotifyCanExecuteChanged();
            DeleteMonitorCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command).ChangeCanExecute();
        }

        public ICommand CancelCommand { get; set; }

        [RelayCommand(CanExecute = nameof(CanEditNewMonitor))]
        public void EditMonitor(MonitorViewModel editableMonitor)
        {
            MonitorEdit = editableMonitor;
            MonitorEdit.PropertyChanged += OnMonitorEditPropertyChanged;
            IsEditing = true;
            IsCreating = false;
            RefreshCanExecute();

        }

        public void RefreshProducers()
        {
            Producers = new ObservableCollection<IProducer>(blc.GetProducers());
            OnPropertyChanged(nameof(GetProducers));
        }

        public void RefreshMonitors()
        {
            Monitors = new ObservableCollection<MonitorViewModel>();
            foreach (var monitor in this.blc.GetMonitors())
            {
                monitors.Add(new MonitorViewModel(monitor));
            }
        }
        [RelayCommand(CanExecute = nameof(CanDeleteMonitor))]
        public void DeleteMonitor()
        {
            blc.DeleteMonitor(MonitorEdit.Id);
            isCreating = false;
            isEditing = false;
            MonitorEdit = null;
            RefreshCanExecute();
            RefreshMonitors();
        }
        private bool CanDeleteMonitor()
        {
            return isEditing == true;
        }
        private bool CanMonitorsBeFiltered()
        {
            return true;
        }

        [RelayCommand(CanExecute = nameof(CanMonitorsBeFiltered))]
        public void FilterMonitors() 
        {
            string filterProducer;
            Monitors.Clear();
            if (MonitorFilter.Producer != null)
            {
                filterProducer = MonitorFilter.Producer.Id.ToString();
            }
            else
            {
                filterProducer = null;
            }
            if(MonitorFilter.MaxDiagonal == 0)
            {
                foreach (var monitor in this.blc.FilterMonitors(MonitorFilter.Name, GetSelectedMatrixFilter, MonitorFilter.MinDiagonal, float.MaxValue, filterProducer))
                {
                    Monitors.Add(new MonitorViewModel(monitor));
                }
            }
            else
            {
                foreach (var monitor in this.blc.FilterMonitors(MonitorFilter.Name, GetSelectedMatrixFilter, MonitorFilter.MinDiagonal, MonitorFilter.MaxDiagonal, filterProducer))
                {
                    Monitors.Add(new MonitorViewModel(monitor));
                }
            }

        }
        [RelayCommand(CanExecute = nameof(CanMonitorsBeFiltered))]
        public void ResetFilter()
        {
            SelectedMatrixFilter = null;
            MonitorFilter = new MonitorViewModel();
            FilterMonitors();
        }
    }
}
