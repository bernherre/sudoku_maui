// Sudoku.Maui/ViewModels/MainViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Sudoku.Maui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private string title = "Sudoku";
    [ObservableProperty] private bool isBusy;

    [RelayCommand]
    private async Task InitAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            // TODO: carga inicial
            await Task.CompletedTask;
        }
        finally { IsBusy = false; }
    }
}
