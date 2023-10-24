using System.Security.Cryptography;
namespace forum.Database
{
    public class Authenticator
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';
        public static byte[] GenerateSalt()
        {
            using (var result = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                result.GetBytes(salt);
                return salt;
            }
        }
        public static string Hash(string password, byte[] salt)
        {
            //var salt = GenerateSalt();
            var hash = Rfc2898DeriveBytes
                .Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);
            return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }
        public static bool Verify(string passwordHash, string inputPassword)
        {
            var elements = passwordHash.Split(Delimiter); 
            var hash = Convert.FromBase64String(elements[1]);
            var salt = Convert.FromBase64String(elements[0]);
            var hashInput = Rfc2898DeriveBytes
                .Pbkdf2(inputPassword, salt, Iterations, _hashAlgorithmName, KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
