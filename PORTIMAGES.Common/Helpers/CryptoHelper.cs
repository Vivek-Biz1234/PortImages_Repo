using System.Security.Cryptography;
using System.Text;

namespace PORTIMAGES.Common.Helpers
{
    public static class CryptoHelper
    {
        // MUST be 32 bytes for AES-256
        private static readonly string SecretKey = "PORTIMAGES_AES_256_KEY_2025!!";
        private static readonly byte[] Salt = Encoding.UTF8.GetBytes("PORTIMAGES_SALT");

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            using var key = new Rfc2898DeriveBytes(SecretKey, Salt, 10000, HashAlgorithmName.SHA256);

            aes.Key = key.GetBytes(32);  // ✅ 256-bit
            aes.IV = key.GetBytes(16);  // ✅ 128-bit

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray())
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", ""); // URL-safe
        }

        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText
                .Replace("-", "+")
                .Replace("_", "/");

            switch (cipherText.Length % 4)
            {
                case 2: cipherText += "=="; break;
                case 3: cipherText += "="; break;
            }

            using var aes = Aes.Create();
            using var key = new Rfc2898DeriveBytes(SecretKey, Salt, 10000, HashAlgorithmName.SHA256);

            aes.Key = key.GetBytes(32);
            aes.IV = key.GetBytes(16);

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
