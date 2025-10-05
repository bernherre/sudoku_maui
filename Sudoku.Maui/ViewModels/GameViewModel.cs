// ViewModels/GameViewModel.cs
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sudoku.Engine; // si usas tu engine en este namespace

namespace Sudoku.Maui.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private readonly System.Timers.Timer _timer;

    // ======= Estado =======
    [ObservableProperty] private int elapsed;           // segundos transcurridos
    [ObservableProperty] private bool isRunning;        // para habilitar/disable botones
    [ObservableProperty] private object? selectedDifficulty; // o tu enum Difficulty

    public GameViewModel()
    {
        _timer = new System.Timers.Timer(1000);
        _timer.AutoReset = true;
        _timer.Elapsed += (_, __) => Elapsed++;
    }

    // ======= Comandos =======
    [RelayCommand]
    private void Start()
    {
        if (IsRunning) return;
        IsRunning = true;
        _timer.Start();
    }

    [RelayCommand]
    private void Pause()
    {
        if (!IsRunning) return;
        IsRunning = false;
        _timer.Stop();
    }

    [RelayCommand]
    private void Reset()
    {
        IsRunning = false;
        _timer.Stop();
        Elapsed = 0;
        // reinicia aquí tu tablero si aplica
    }

    // Llamado desde la UI (Picker/RadioButton) para cambiar dificultad
    public void SetDifficultyFromObject(object value)
    {
        // adapta a tu tipo real de dificultad
        SelectedDifficulty = value;
    }
}
