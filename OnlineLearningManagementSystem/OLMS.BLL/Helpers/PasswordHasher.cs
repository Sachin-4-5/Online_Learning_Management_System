using System;
using System.Security.Cryptography;

namespace OLMS.BLL.Helpers
{
    public static class PasswordHasher
    {
        // Format: {iterations}.{saltBase64}.{hashBase64}
        private const int Iterations = 100_000;
        private const int SaltSize = 16;   // 128-bit
        private const int KeySize = 32;    // 256-bit

        public static string Hash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    var key = pbkdf2.GetBytes(KeySize);
                    return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
                }
            }
        }

        public static bool Verify(string password, string hashed)
        {
            if (string.IsNullOrWhiteSpace(hashed)) return false;

            var parts = hashed.Split('.');
            if (parts.Length != 3) return false;

            var iterations = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                var computed = pbkdf2.GetBytes(key.Length);
                return FixedTimeEquals(computed, key);
            }
        }

        /// <summary>
        /// Fixed-time comparison to prevent timing attacks (compatible with .NET Framework)
        /// </summary>
        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }

    }
}
