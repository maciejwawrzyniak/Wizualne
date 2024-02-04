using TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels;

namespace TomaszewskiWawrz.MonitoryApp.MAUI;

public partial class ProducersPage : ContentPage
{
	public ProducersPage(ProducersCollectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var producerViewModel = (e.Item as ProducerViewModel).Clone() as ProducerViewModel;
        //monitorViewModel.Producer = (BindingContext as MonitorsCollectionViewModel).AllPublishers.FirstOrDefault(p => p.ID == bookViewModel.Publisher.ID);
        //(BindingContext as BookCollectionViewModel).RefreshPublishers();
        //(BindingContext as BookCollectionViewModel).EditBook(bookViewModel);

    }
}