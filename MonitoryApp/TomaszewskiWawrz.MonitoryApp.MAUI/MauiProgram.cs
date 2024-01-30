using Microsoft.Extensions.Logging;
using TomaszewskiWawrz.MonitoryApp.MAUI.ViewModels;
using TomaszewskiWawrzyniak.MonitoryApp.BLC;
using TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1;

namespace TomaszewskiWawrz.MonitoryApp.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<MonitorsCollectionViewModel>();
            builder.Services.AddSingleton<MonitoryPage>();
            builder.Services.AddDbContext<DAO>();
            builder.Services.AddSingleton<BLC>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
