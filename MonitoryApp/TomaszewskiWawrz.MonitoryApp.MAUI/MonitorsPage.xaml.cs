using TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels;

namespace TomaszewskiWawrz.MonitoryApp.MAUI;

public partial class MonitorsPage : ContentPage
{
	public MonitorsPage(MonitorsCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}