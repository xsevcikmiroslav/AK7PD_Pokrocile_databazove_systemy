using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Password
{
    internal class PasswordHelper
    {
        internal static (byte[], byte[]) GetPasswordHash(string password)
        {
            // divide by 8 to convert bits to bytes
            var salt = RandomNumberGenerator.GetBytes(128 / 8);

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            var hashed = KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);

            return (salt, hashed);
        }
    }
}
