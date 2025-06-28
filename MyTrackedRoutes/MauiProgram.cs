using Microsoft.Extensions.Logging;
using MyTrackedRoutes.Services;
using MyTrackedRoutes.Services.Interfaces;

namespace MyTrackedRoutes;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<ILocationService, LocationService>();

        return builder.Build();
    }
}