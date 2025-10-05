using CommunityToolkit.Maui;                     // si usas el toolkit
using Microsoft.Extensions.DependencyInjection; // DI
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.Logging;
using Sudoku.Maui.ViewModels; // <- IMPORTANTE
using Sudoku.Maui;

namespace Sudoku.Maui;

public static class ServiceHelper
{
    public static IServiceProvider Services { get; private set; } = default!;
    public static void Initialize(IServiceProvider services) => Services = services;
}

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit() // quítalo si no usas el toolkit
            .ConfigureFonts(fonts =>
            {
                // fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // DI: páginas y viewmodels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ViewModels.GameViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
