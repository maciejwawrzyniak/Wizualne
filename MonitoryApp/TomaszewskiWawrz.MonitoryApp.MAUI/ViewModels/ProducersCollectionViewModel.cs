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

namespace TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels
{
    public partial class ProducersCollectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ProducerViewModel> producers;

        private BLC blc;

        [ObservableProperty]
        private ProducerViewModel producerEdit;

        [ObservableProperty]
        private bool isEditing;

        ProducersCollectionViewModel(BLC blc)
        {
            this.blc = blc;
            Producers = new ObservableCollection<ProducerViewModel>();
            foreach (var producer in this.blc.GetProducers())
            {
                producers.Add(new ProducerViewModel(producer));
            }
            IsEditing = false;
            ProducerEdit = null;

            CancelCommand = new Command(
                execute: () =>
                {
                    ProducerEdit.PropertyChanged -= OnProducerEditPropertyChanged;
                    ProducerEdit = null;
                    isEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return isEditing;
                });
        }
        [RelayCommand(CanExecute = nameof(CanCreateNewProducer))]
        private void CreateNewProducer()
        {
            ProducerEdit = new ProducerViewModel();
            ProducerEdit.PropertyChanged += OnProducerEditPropertyChanged;
            IsEditing = true;

            RefreshCanExecute();
        }
        private bool CanCreateNewProducer()
        {
            return !IsEditing;
        }
        [RelayCommand(CanExecute = nameof(CanEditProducerBeSaved))]
        private void SaveProducer()
        {
            Producers.Add(producerEdit);
            ProducerEdit.PropertyChanged -= OnProducerEditPropertyChanged;
            ProducerEdit = null;
            IsEditing = false;
            RefreshCanExecute();
        }

        private bool CanEditProducerBeSaved()
        {
            return ProducerEdit != null &&
                   ProducerEdit.Name != null &&
                   ProducerEdit.Name.Length > 1;
        }
        private void RefreshCanExecute()
        {
            CreateNewProducerCommand.NotifyCanExecuteChanged();
            SaveProducerCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command).ChangeCanExecute();
        }
        private void OnProducerEditPropertyChanged(object Sender, PropertyChangedEventArgs args)
        {
            SaveProducerCommand.NotifyCanExecuteChanged();
        }

        public ICommand CancelCommand { get; set; }
    }
}
