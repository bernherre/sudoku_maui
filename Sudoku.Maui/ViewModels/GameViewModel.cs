// Sudoku.Maui/ViewModels/GameViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using Microsoft.Maui.Dispatching; // <- necesario para IDispatcherTimer

namespace Sudoku.Maui.ViewModels;

// Evita cualquier Timer del BCL: NO agregues using System.Timers ni using System.Threading
public partial class GameViewModel : ObservableObject
{
    private readonly IDispatcherTimer _timer;

    [ObservableProperty] private bool isRunning;
    [ObservableProperty] private TimeSpan elapsed = TimeSpan.Zero;

    public GameViewModel()
    {
        // Application.Current!.Dispatcher siempre existe en MAUI
        _timer = Application.Current!.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.IsRepeating = true;
        _timer.Tick += (_, __) =>
        {
            // Ya estamos en el hilo de UI
            Elapsed = Elapsed.Add(TimeSpan.FromSeconds(1));
        };
    }

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
        Elapsed = TimeSpan.Zero;
    }
}
