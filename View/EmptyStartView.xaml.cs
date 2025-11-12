using System;
using System.Windows;
using System.Windows.Controls;
using SPZO.Services;

namespace SPZO.View
{
    public partial class EmptyStartView : UserControl
    {
        public static readonly DependencyProperty IsAuthenticatedProperty =
            DependencyProperty.Register(nameof(IsAuthenticated), typeof(bool), typeof(EmptyStartView), new PropertyMetadata(false, OnIsAuthenticatedChanged));

        public bool IsAuthenticated
        {
            get => (bool)GetValue(IsAuthenticatedProperty);
            set => SetValue(IsAuthenticatedProperty, value);
        }

        public event EventHandler<bool>? AuthenticationChanged;

        public EmptyStartView()
        {
            InitializeComponent();

            // Keep local DP in sync with the global AuthenticationService
            AuthenticationService.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(AuthenticationService.IsAuthenticated))
                {
                    if (IsAuthenticated != AuthenticationService.Instance.IsAuthenticated)
                        IsAuthenticated = AuthenticationService.Instance.IsAuthenticated;

                    // wyczyść pola po wylogowaniu
                    if (!AuthenticationService.Instance.IsAuthenticated)
                    {
                        // wyczyść pola tekstowe
                        Dispatcher.Invoke(() =>
                        {
                            UsernameTextBox.Clear();
                            PasswordBox.Clear();
                            UsernameTextBox.Focus();
                        });
                    }
                }
            };
        }

        private static void OnIsAuthenticatedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (EmptyStartView)d;
            view.AuthenticationChanged?.Invoke(view, (bool)e.NewValue);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = UsernameTextBox.Text?.Trim();
            var pass = PasswordBox.Password;

            // Use AuthenticationService to validate against Users table
            if (AuthenticationService.Instance.Authenticate(user, pass))
            {
                IsAuthenticated = true;
            }
            else
            {
                MessageBox.Show("Nieprawidłowa nazwa użytkownika lub hasło.", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Clear();
            PasswordBox.Clear();
            UsernameTextBox.Focus();
        }
    }
}
