using System.Security.Cryptography;
using System.Text;

namespace DataLayerDTO.DTO
{
    public enum AccountState
    {
        AwatingApproval = 1,
        Active,
        Banned,
        Deleted
    }

    public class UserDto : BaseDto
    {
        public string Firstname { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Pin { get; set; } = string.Empty;

        public AddressDto Address { get; set; } = new AddressDto();

        public string Username { get; set; } = string.Empty;

        public byte[] Salt { get; set; } = new byte[] { };

        public byte[] Hash { get; set; } = new byte[] {};

        public int AccountState { get; set; }

        public bool IsAdmin { get; set; }

        public static (byte[], byte[]) GetPasswordHash(string password)
        {
            var data = Encoding.UTF8.GetBytes(password);
            using SHA512 shaM = SHA512.Create();
            return (shaM.ComputeHash(data), shaM.ComputeHash(data));
        }

        /*
        public static (byte[], byte[]) GetPasswordHash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
        */
    }
}
