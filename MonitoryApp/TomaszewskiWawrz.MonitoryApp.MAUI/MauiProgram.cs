using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels;
using TomaszewskiWawrzyniak.MonitoryApp.BLC;

namespace TomaszewskiWawrz.MonitoryApp.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<MonitorsCollectionViewModel>();
            builder.Services.AddSingleton<MonitorViewModel>();
            builder.Services.AddSingleton<BLC>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
