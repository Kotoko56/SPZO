using SPZO.ViewModel;
using System.Windows;
using SPZO.View;
using SPZO.Services;

namespace SPZO
{
    public partial class MainWindow : Window
    {
        public MainWindow() 
        { 
            InitializeComponent(); 

            DataContext = new MainWindowViewModel();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Log out global state
            AuthenticationService.Instance.Logout();

            if (DataContext is MainWindowViewModel vm)
            {
                vm.SelectedViewModel = new EmptyStartViewModel();
            }
        }
    }
}