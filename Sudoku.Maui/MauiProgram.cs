using CommunityToolkit.Maui;      // <- ahora sí existe
using Microsoft.Extensions.Logging;
using Sudoku.Maui.ViewModels;

namespace Sudoku.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()   // <- solo si instalaste el paquete
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // DI
        builder.Services.AddSingleton<GameViewModel>();
        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
    }
}
