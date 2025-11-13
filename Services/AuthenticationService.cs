using System.ComponentModel;
using System.Linq;
using SPZO.DataManagement;
using SPZO.Model;

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
            // ensure DB exists and create/migrate default user if needed
            EnsureDefaultUser();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // Authenticate against Users table using PBKDF2 verification.
        public bool Authenticate(string? username, string? password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return false;

            using var db = new SQLiteDataAccess();
            db.Database.EnsureCreated();

            var user = db.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
                return Fail();

            // Legacy path: if old plaintext field still exists, allow one-time login and migrate.
            if ((user.PasswordHash == null || user.PasswordHash.Length == 0) && !string.IsNullOrEmpty(user.UserPassword))
            {
                if (password == user.UserPassword)
                {
                    var (hash, salt, iters) = PasswordHasher.Hash(password);
                    user.PasswordHash = hash;
                    user.PasswordSalt = salt;
                    user.PasswordIterations = iters;
                    user.UserPassword = null; // clear legacy field
                    db.SaveChanges();
                    return Succeed(user.UserName);
                }
                return Fail();
            }

            // Normal PBKDF2 verification
            if (user.PasswordHash != null && user.PasswordSalt != null && user.PasswordIterations > 0)
            {
                var ok = PasswordHasher.Verify(password, user.PasswordHash, user.PasswordSalt, user.PasswordIterations);
                return ok ? Succeed(user.UserName) : Fail();
            }

            return Fail();

            bool Succeed(string name)
            {
                CurrentUser = name;
                IsAuthenticated = true;
                return true;
            }

            bool Fail()
            {
                CurrentUser = null;
                IsAuthenticated = false;
                return false;
            }
        }

        public void Logout()
        {
            CurrentUser = null;
            IsAuthenticated = false;
        }

        // Create default admin user with PBKDF2 if DB empty,
        // or migrate legacy plaintext password to PBKDF2.
        private void EnsureDefaultUser()
        {
            using var db = new SQLiteDataAccess();
            db.Database.EnsureCreated();

            var admin = db.Users.FirstOrDefault(u => u.UserName == "admin");
            if (admin == null)
            {
                var (hash, salt, iters) = PasswordHasher.Hash("admin");
                db.Users.Add(new Users
                {
                    UserName = "admin",
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    PasswordIterations = iters
                });
                db.SaveChanges();
            }
            else if ((admin.PasswordHash == null || admin.PasswordHash.Length == 0) && !string.IsNullOrEmpty(admin.UserPassword))
            {
                // migrate old plaintext to PBKDF2 on startup if still present
                var (hash, salt, iters) = PasswordHasher.Hash(admin.UserPassword);
                admin.PasswordHash = hash;
                admin.PasswordSalt = salt;
                admin.PasswordIterations = iters;
                admin.UserPassword = null;
                db.SaveChanges();
            }
        }
    }
}