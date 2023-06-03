using Microsoft.Extensions.Logging;

namespace NoticiaMAUI
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
                    fonts.AddFont("kenycb.ttf", "keny");
                    fonts.AddFont("built-bold.otf", "BuiltBold");
                    fonts.AddFont("built-regular.otf", "BuiltRegular");
                    fonts.AddFont("Geologica-Light.ttf", "GeologicaLight");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}