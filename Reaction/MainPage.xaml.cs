using System.Windows.Controls;
using System.Windows.Input;

namespace Reaction
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((MainViewModel)DataContext).HandleInputCommand.Execute(null);
        }
    }
}
