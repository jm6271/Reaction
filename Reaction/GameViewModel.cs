using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;

namespace Reaction
{
    public partial class GameViewModel : ObservableObject
    {
        private static readonly SolidColorBrush NormalBackgroundBrush =
            new(Color.FromArgb(0xFF, 0x2B, 0x87, 0xD1));
        private static readonly SolidColorBrush WaitingBackgroundBrush =
            new(Color.FromArgb(0xFF, 0xCE, 0x26, 0x36));
        private static readonly SolidColorBrush PressBackgroundBrush =
            new(Color.FromArgb(0xFF, 0x4B, 0xDB, 0x6A));

        private readonly Random _random = new();
        private readonly Stopwatch _sw = new();

        private enum AppState
        {
            PrePlay,
            WaitForSignal,
            WaitForKeyPress,
            GameOver,
        }

        private AppState _state = AppState.PrePlay;

        [ObservableProperty]
        private Brush _backgroundColor = NormalBackgroundBrush;

        [ObservableProperty]
        private string _titleText = "Reaction Time Test";

        [ObservableProperty]
        private string _bodyLine1 = "When the background turns green, click as quickly as you can!";

        [ObservableProperty]
        private string _bodyLine2 = "Click to start.";

        [RelayCommand]
        private void HandleInput()
        {
            switch (_state)
            {
                case AppState.PrePlay:
                    _state = AppState.WaitForSignal;
                    BackgroundColor = WaitingBackgroundBrush;
                    TitleText = "Wait for green...";
                    BodyLine1 = string.Empty;
                    BodyLine2 = string.Empty;
                    _ = StartGame();
                    break;
                case AppState.WaitForKeyPress:
                    _sw.Stop();
                    _state = AppState.GameOver;
                    BackgroundColor = NormalBackgroundBrush;
                    TitleText = $"{_sw.ElapsedMilliseconds} ms";
                    BodyLine1 = "Click to start over.";
                    BodyLine2 = string.Empty;
                    break;
                case AppState.GameOver:
                    _state = AppState.PrePlay;
                    BackgroundColor = NormalBackgroundBrush;
                    TitleText = "Reaction Time Test";
                    BodyLine1 = "When the background turns green, click as quickly as you can!";
                    BodyLine2 = "Click to start.";
                    break;
                case AppState.WaitForSignal:
                    // User pressed too early
                    _state = AppState.GameOver;
                    BackgroundColor = NormalBackgroundBrush;
                    TitleText = "Too soon!";
                    BodyLine1 = "Click to try again.";
                    BodyLine2 = string.Empty;
                    break;
            }
        }

        private async Task StartGame()
        {
            var waitTime = _random.Next(2000, 5000);
            await Task.Delay(waitTime);

            // Prevent a race condition by making sure the state hasn't changed while we were waiting
            if (_state != AppState.WaitForSignal)
            {
                return;
            }

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;

            BackgroundColor = PressBackgroundBrush;
            TitleText = "Go!";
            _state = AppState.WaitForKeyPress;

            _sw.Restart();
        }
    }
}
