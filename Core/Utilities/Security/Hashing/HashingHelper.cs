#nullable enable
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreateHash(string inputText, out byte[] outputHash, out byte[] outputSalt)
        {
            using (var hmac = GetHmacInstance(null))
            {
                outputSalt = hmac.Key;
                outputHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputText));
            }
        }

        public static bool VerifyHash(string inputText, byte[] currentHash, byte[] salt)
        {
            using (var hmac = GetHmacInstance(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputText));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != currentHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Private Methods

        private static HMAC GetHmacInstance(byte[]? salt)
        {
            return salt == null
                ? new HMACSHA512()
                : new HMACSHA512(salt);
        }
    }
}
