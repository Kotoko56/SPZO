using System;
using System.Security.Cryptography;

namespace SPZO.Services
{
    public static class PasswordHasher
    {
        public const int SaltSize = 16;        // 128-bit
        public const int KeySize = 32;        // 256-bit
        public const int DefaultIterations = 500_000;

        public static (byte[] hash, byte[] salt, int iterations) Hash(string password, int? iterations = null)
        {
            var iters = iterations ?? DefaultIterations;
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iters,
                HashAlgorithmName.SHA256,
                KeySize);
            return (hash, salt, iters);
        }

        public static bool Verify(string password, byte[] storedHash, byte[] storedSalt, int iterations)
        {
            var computed = Rfc2898DeriveBytes.Pbkdf2(
                password,
                storedSalt,
                iterations,
                HashAlgorithmName.SHA256,
                storedHash.Length);
            return CryptographicOperations.FixedTimeEquals(computed, storedHash);
        }
    }
}