//using Bumptech.Glide.Manager;
using TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels;

namespace TomaszewskiWawrz.MonitoryApp.MAUI;

public partial class MonitorsPage : ContentPage
{
	public MonitorsPage(MonitorsCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        viewModel.RefreshProducers();
        viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(viewModel.Monitors))
            {
                var updatedMonitors = viewModel.Monitors;
                MonitorListView.ItemsSource = updatedMonitors;
            }
        };
	}

    protected override void OnAppearing()
    {
        ((MonitorsCollectionViewModel)BindingContext).RefreshProducers();
        ((MonitorsCollectionViewModel)BindingContext).RefreshMonitors();
        base.OnAppearing();
    }

    void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var monitorViewModel = (e.Item as MonitorViewModel).Clone() as MonitorViewModel;
        monitorViewModel.Producer = (BindingContext as MonitorsCollectionViewModel).GetProducers.FirstOrDefault(p => p.Id == monitorViewModel.Producer.Id);
        (BindingContext as MonitorsCollectionViewModel).RefreshProducers();
        (BindingContext as MonitorsCollectionViewModel).EditMonitor(monitorViewModel);

    }
}