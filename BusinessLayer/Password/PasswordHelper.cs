using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BusinessLayer.Password
{
    internal static class PasswordHelper
    {
        internal static (byte[], byte[]) HashNewPassword(string password)
        {
            // divide by 8 to convert bits to bytes
            var salt = RandomNumberGenerator.GetBytes(128 / 8);

            var hashed = HashPassword(salt, password);

            return (salt, hashed);
        }

        internal static byte[] HashPassword(byte[] salt, string password)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            return
                KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);
        }


    }
}
