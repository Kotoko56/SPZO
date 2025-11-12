using System.Windows;
using SPZO.Services;

namespace SPZO.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            UsernameTextBox.Focus();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text?.Trim();
            var password = PasswordBox.Password;

            if (AuthenticationService.Instance.Authenticate(username, password))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Nieprawid³owa nazwa u¿ytkownika lub has³o.", "B³¹d logowania", MessageBoxButton.OK, MessageBoxImage.Warning);
                PasswordBox.Clear();
                PasswordBox.Focus();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}