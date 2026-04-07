using System.Windows;

namespace Reaction
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new GamePage());
        }
    }
}
