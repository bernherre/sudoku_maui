using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Sudoku.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    // Forma recomendada en MAUI para todas las plataformas
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var mainPage = ServiceHelper.Services.GetRequiredService<MainPage>();
        return new Window(mainPage);
    }
}
