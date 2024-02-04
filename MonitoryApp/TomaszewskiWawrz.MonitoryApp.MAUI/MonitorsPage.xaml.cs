using TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels;

namespace TomaszewskiWawrz.MonitoryApp.MAUI;

public partial class MonitorsPage : ContentPage
{
	public MonitorsPage(MonitorsCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var monitorViewModel = (e.Item as MonitorViewModel).Clone() as MonitorViewModel;
        //monitorViewModel.Producer = (BindingContext as MonitorsCollectionViewModel).AllPublishers.FirstOrDefault(p => p.ID == bookViewModel.Publisher.ID);
        //(BindingContext as BookCollectionViewModel).RefreshPublishers();
        //(BindingContext as BookCollectionViewModel).EditBook(bookViewModel);

    }
}