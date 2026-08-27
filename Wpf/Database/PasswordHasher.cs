using System;
using System.Security.Cryptography;

namespace Database
{
    /// <summary>
    /// PBKDF2 기반 비밀번호 해시/검증. 저장 형식: {iterations}.{salt-base64}.{hash-base64}
    /// </summary>
    public class PasswordHasher
    {
        readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

        public string Hash(string password)
        {
            const int SaltSize = 16;
            const int HashSize = 32;
            const int Iterations = 100_000;

            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _algorithm, HashSize);
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool Verify(string password, string stored)
        {
            var parts = stored.Split('.', 3);
            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], out var iterations))
                return false;

            var salt = Convert.FromBase64String(parts[1]);
            var expectedHash = Convert.FromBase64String(parts[2]);

            var actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, _algorithm, expectedHash.Length);
            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
