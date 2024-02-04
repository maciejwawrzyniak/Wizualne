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
    public partial class ProducersCollectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ProducerViewModel> producers;

        private BLC blc;



        public ProducersCollectionViewModel(BLC blc)
        {
            this.blc = blc;
            Producers = new ObservableCollection<ProducerViewModel>();
            foreach (var producer in this.blc.GetProducers())
            {
                producers.Add(new ProducerViewModel(producer));
            }
            IsEditing = false;
            IsCreating = false;
            ProducerEdit = null;
            ProducerFilter = new ProducerViewModel();

            CancelCommand = new Command(
                execute: () =>
                {
                    ProducerEdit.PropertyChanged -= OnProducerEditPropertyChanged;
                    ProducerEdit = null;
                    isEditing = false;
                    isCreating = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return isEditing || isCreating;
                });
        }
        [ObservableProperty]
        private ProducerViewModel producerEdit;
        [ObservableProperty]
        private ProducerViewModel producerFilter;

        [ObservableProperty]
        private bool isEditing = false;
        [ObservableProperty]
        private bool isCreating = false;
        [RelayCommand(CanExecute = nameof(CanCreateNewProducer))]
        private void CreateNewProducer()
        {
            ProducerEdit = new ProducerViewModel();
            ProducerEdit.PropertyChanged += OnProducerEditPropertyChanged;
            IsCreating = true;
            IsEditing = false;

            RefreshCanExecute();
        }
        private bool CanCreateNewProducer()
        {
            return !IsCreating;
        }
        private bool CanEditNewProducer()
        {
            return !IsEditing;
        }
        [RelayCommand(CanExecute = nameof(CanEditProducerBeSaved))]
        private void SaveProducer()
        {
            if (IsCreating)
            {
                var producer = blc.CreateNewProducer(ProducerEdit.Name, ProducerEdit.CountryFrom);
            }
            else
            {
                blc.EditProducer(ProducerEdit.Id, ProducerEdit.Name, ProducerEdit.CountryFrom);
            }
            
            ProducerEdit.PropertyChanged -= OnProducerEditPropertyChanged;
            ProducerEdit = null;
            IsEditing = false;
            IsCreating = false;
            RefreshCanExecute();
            ReloadProducers();
        }

        private bool CanEditProducerBeSaved()
        {
            return ProducerEdit != null &&
                   ProducerEdit.Name != null &&
                   ProducerEdit.Name.Length > 1 &&
                   ProducerEdit.CountryFrom != null &&
                   ProducerEdit.CountryFrom.Length > 1;
        }
        [RelayCommand(CanExecute = nameof(CanEditNewProducer))]
        public void EditProducer(ProducerViewModel producer)
        {
            ProducerEdit = producer;
            ProducerEdit.PropertyChanged += OnProducerEditPropertyChanged;
            IsEditing = true;
            IsCreating = false;
            RefreshCanExecute();
        }
        private void RefreshCanExecute()
        {
            CreateNewProducerCommand.NotifyCanExecuteChanged();
            SaveProducerCommand.NotifyCanExecuteChanged();
            EditProducerCommand.NotifyCanExecuteChanged();
            DeleteProducerCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command).ChangeCanExecute();
        }
        private void OnProducerEditPropertyChanged(object Sender, PropertyChangedEventArgs args)
        {
            SaveProducerCommand.NotifyCanExecuteChanged();
            CreateNewProducerCommand.NotifyCanExecuteChanged();
            DeleteProducerCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command).ChangeCanExecute();

        }
        void ReloadProducers()
        {
            Producers.Clear();
            foreach (IProducer producer in blc.GetProducers())
            {
                Producers.Add(new ProducerViewModel(producer));
            }
            OnPropertyChanged(nameof(Producers));
        }
        [RelayCommand(CanExecute = nameof(CanDeleteProducer))]
        public void DeleteProducer()
        {
            blc.DeleteProducer(ProducerEdit.Id);
            IsCreating = false;
            IsEditing = false;
            ProducerEdit = null;
            RefreshCanExecute();
            ReloadProducers();
        }
        private bool CanDeleteProducer()
        {
            return IsEditing == true;
        }

        public ICommand CancelCommand { get; set; }
        private bool CanProducersBeFiltered()
        {
            return true;
        }

        [RelayCommand(CanExecute = nameof(CanProducersBeFiltered))]
        public void FilterProducers()
        {
            Producers.Clear();
            foreach (var producer in this.blc.FilterProducers(producerFilter.Name, producerFilter.CountryFrom))
            {
                Producers.Add(new ProducerViewModel(producer));
            }

        }
        [RelayCommand(CanExecute = nameof(CanProducersBeFiltered))]
        public void ResetFilter()
        {
            ProducerFilter = new ProducerViewModel();
            FilterProducers();
        }
    }
}
