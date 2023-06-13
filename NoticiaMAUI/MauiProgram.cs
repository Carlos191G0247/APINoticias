using Microsoft.Extensions.Logging;
using NoticiaMAUI.Service;
using NoticiaMAUI.ViewModels;

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
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                });
            builder.Services.AddSingleton<AuthService> ();
            builder.Services.AddSingleton<LoginService> ();
            builder.Services.AddSingleton<ViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}