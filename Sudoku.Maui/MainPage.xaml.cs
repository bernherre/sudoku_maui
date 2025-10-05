// MainPage.xaml.cs
using System;
using Microsoft.Maui.Controls;
using Sudoku.Maui.ViewModels;

namespace Sudoku.Maui;

public partial class MainPage : ContentPage
{
    public MainPage(GameViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    // Firma válida para SizeChanged
    private void OnPageSizeChanged(object? sender, EventArgs e)
    {
        // opcional: recalcula tamaños del grid, etc.
        // ej: var width = (sender as VisualElement)?.Width;
    }

    // Si además usas el Picker con DifficultyChanged:
    private void DifficultyChanged(object? sender, EventArgs e)
    {
        if (BindingContext is GameViewModel vm)
        {
            if (sender is Picker p && p.SelectedItem is not null)
            {
                vm.SetDifficultyFromObject(p.SelectedItem);
            }
            else if (sender is RadioButton rb && rb.IsChecked && rb.Value is not null)
            {
                vm.SetDifficultyFromObject(rb.Value);
            }
        }
    }
}
