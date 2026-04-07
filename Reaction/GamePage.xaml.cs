using System.Windows.Controls;
using System.Windows.Input;

namespace Reaction
{
    public partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((GameViewModel)DataContext).HandleInputCommand.Execute(null);
        }
    }
}
