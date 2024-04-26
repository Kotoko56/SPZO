using SPZO.ViewModel;
using System.Windows;

namespace SPZO
{
    public partial class MainWindow : Window
    {
        public MainWindow() 
        { 
            InitializeComponent(); 

            DataContext = new MainWindowViewModel(); //Define property DataContext to MainWindowViewModel. Every time, when I switch between viewmodel, it's assigned to MainWindowsViewmodel
        }
    }
}