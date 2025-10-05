using CommunityToolkit.Mvvm.ComponentModel;

namespace Sudoku.Maui.ViewModels;

public partial class CellVM : ObservableObject
{
    [ObservableProperty] private int row;
    [ObservableProperty] private int col;
    [ObservableProperty] private int? value;
    [ObservableProperty] private bool isGiven;
}
