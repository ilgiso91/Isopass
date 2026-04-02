using System.Security.Cryptography;
using System.Text;

namespace Isopass.Services
{
    public class EncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(string masterPassword)
        {
            _key = SHA256.HashData(Encoding.UTF8.GetBytes(masterPassword));
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            byte[] encrypted = encryptor.TransformFinalBlock(
                Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

            byte[] combined = aes.IV.Concat(encrypted).ToArray();
            return Convert.ToBase64String(combined);
        }

        public string Decrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = _key;

            byte[] iv = fullCipher.Take(16).ToArray();
            byte[] cipher = fullCipher.Skip(16).ToArray();

            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            byte[] decrypted = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);

            return Encoding.UTF8.GetString(decrypted);
        }

        public static string HashMaster(string masterPassword)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(masterPassword));
            return Convert.ToBase64String(bytes);
        }
    }
}