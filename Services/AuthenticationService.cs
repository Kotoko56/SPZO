using System.ComponentModel;
using System.Linq;
using SPZO.DataManagement;
using SPZO.Model;
using Microsoft.EntityFrameworkCore; // added

namespace SPZO.Services
{
    // Global authentication state singleton that supports data binding
    public class AuthenticationService : INotifyPropertyChanged
    {
        public static AuthenticationService Instance { get; } = new AuthenticationService();

        private bool isAuthenticated;
        private string? currentUser;

        public bool IsAuthenticated
        {
            get => isAuthenticated;
            private set
            {
                if (isAuthenticated == value) return;
                isAuthenticated = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAuthenticated)));
            }
        }

        public string? CurrentUser
        {
            get => currentUser;
            private set
            {
                if (currentUser == value) return;
                currentUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentUser)));
            }
        }

        private AuthenticationService()
        {
            // ensure DB exists and create default user if needed (dev convenience)
            EnsureDefaultUser();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // Authenticate against Users table (plaintext comparison with stored UserPassword).
        // Returns true if authentication succeeded and updates IsAuthenticated/CurrentUser.
        public bool Authenticate(string? username, string? password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return false;

            using var db = new SQLiteDataAccess();

            // ensure DB/schema present before querying
            db.Database.EnsureCreated();

            // Query for exact match in Users table
            var user = db.Users.FirstOrDefault(u => u.UserName == username && u.UserPassword == password);
            if (user != null)
            {
                CurrentUser = user.UserName;
                IsAuthenticated = true;
                return true;
            }

            // failed
            CurrentUser = null; 
            IsAuthenticated = false;
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
            IsAuthenticated = false;
        }

        // Development helper: create a default admin/admin user if Users table empty.
        // Remove or change for production and use hashed passwords.
        private void EnsureDefaultUser()
        {
            using var db = new SQLiteDataAccess();

            // Ensure database and schema are created where possible
            db.Database.EnsureCreated();

            // If the DB existed but is missing the Users table, EnsureCreated may do nothing.
            // If you still hit 'no such table' after this, you must run migrations or recreate the DB file.
            if (!db.Users.Any())
            {
                db.Users.Add(new Users { UserName = "admin", UserPassword = "admin" });
                db.SaveChanges();
            }
        }
    }
}