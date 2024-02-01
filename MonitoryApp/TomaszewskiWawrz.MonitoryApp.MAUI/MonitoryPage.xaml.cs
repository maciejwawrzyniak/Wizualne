using TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels;

namespace TomaszewskiWawrz.MonitoryApp.MAUI;

public partial class MonitoryPage : ContentPage
{
    public MonitoryPage(MonitorsCollectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}