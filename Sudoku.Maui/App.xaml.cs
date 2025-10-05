using Microsoft.Maui.Controls;

namespace Sudoku.Maui;

public partial class App : Application
{
    private readonly IServiceProvider _sp;

    public App(IServiceProvider sp)
    {
        InitializeComponent();
        _sp = sp;
    }

    // Aquí reemplazas el MainPage obsoleto
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var mainPage = _sp.GetRequiredService<MainPage>();
        return new Window(mainPage);
    }
}
