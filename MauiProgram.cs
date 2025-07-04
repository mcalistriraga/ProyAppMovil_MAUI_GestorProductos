using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace MauiAppGestorMovil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit() // <-- Registro del toolkit
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    // Registro de las fuentes Inter
                    fonts.AddFont("InterVariable.ttf", "Inter");
                    fonts.AddFont("InterVariable-Italic.ttf", "InterItalic");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
