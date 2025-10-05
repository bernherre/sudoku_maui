using Sudoku.Maui.ViewModels;

namespace Sudoku.Maui;

public partial class MainPage : ContentPage
{
    public MainPage(GameViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
