using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Reaction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly Color NormalBackground = Color.FromArgb(0xFF, 0x2B, 0x87, 0xD1);
        private readonly Color WaitingBackground = Color.FromArgb(0xFF, 0xCE, 0x26, 0x36);
        private readonly Color PressBackground = Color.FromArgb(0xFF, 0x4B, 0xDB, 0x6A);

        private readonly SolidColorBrush NormalBackgroundBrush;
        private readonly SolidColorBrush WaitingBackgroundBrush;
        private readonly SolidColorBrush PressBackgroundBrush;

        private Random _random = new();

        private Brush _backgroundColorBrush;
        private string _resultText = string.Empty;
        private string _titleText = "Reaction Time Test";
        private string _bodyLine1 = "When the background turns green, click as quickly as you can!";
        private string _bodyLine2 = "Click to start.";

        public MainWindow()
        {
            NormalBackgroundBrush = new SolidColorBrush(NormalBackground);
            WaitingBackgroundBrush = new SolidColorBrush(WaitingBackground);
            PressBackgroundBrush = new SolidColorBrush(PressBackground);

            _backgroundColorBrush = NormalBackgroundBrush;
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Brush BackgroundColor
        {
            get
            {
                return _backgroundColorBrush;
            }
            set
            {
                _backgroundColorBrush = value;
                OnPropertyChanged();
            }
        }

        public string ResultText
        {
            get
            {
                return _resultText;
            }
            set
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        public string TitleText
        {
            get { return _titleText; }
            set
            {
                _titleText = value;
                OnPropertyChanged();
            }
        }

        public string BodyLine1
        {
            get { return _bodyLine1; }
            set
            {
                _bodyLine1 = value;
                OnPropertyChanged();
            }
        }

        public string BodyLine2
        {
            get { return _bodyLine2; }
            set
            {
                _bodyLine2 = value;
                OnPropertyChanged();
            }
        }

        private enum AppState
        {
            PrePlay,
            WaitForSignal,
            WaitForKeyPress,
            GameOver,
        }

        private AppState _state = AppState.PrePlay;

        private Stopwatch _sw = new();

        /// <Summary>
        /// This should be run on a background thread
        /// </Summary>
        private async Task StartGame()
        {
            // Pick a random amount of time to wait between 2 and 5 seconds
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

            // Switch background to green and wait for key press
            BackgroundColor = PressBackgroundBrush;
            TitleText = "Go!";
            _state = AppState.WaitForKeyPress;

            // Start stopwatch
            _sw.Restart();
        }

        private void StopGame()
        {
            _sw.Stop();
            _state = AppState.GameOver;
            ResultText  = $"{_sw.ElapsedMilliseconds} ms";
        }

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
                    _state = AppState.GameOver;
                    BackgroundColor = NormalBackgroundBrush;
                    StopGame();
                    TitleText = ResultText;
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HandleInput();
        }
    }
}
