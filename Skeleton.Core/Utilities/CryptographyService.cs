using System;
using System.Text;

namespace Skeleton.Core.Utilities
{
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    public static class CryptographyService
    {
        public static string HashedValueSha256(string value, string secretKey)
        {
            var builder = new StringBuilder();

            string combinedValue = value + secretKey;

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(combinedValue));

                foreach (byte b in result)
                {
                    builder.Append(b.ToString("x2"));
                }
            }

            return builder.ToString();
        }

        public static string HashedValueSha512(string value, string secretKey)
        {
            var builder = new StringBuilder();

            string combinedValue = value + secretKey;

            using (SHA512 hash = SHA512.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(combinedValue));

                foreach (byte b in result)
                {
                    builder.Append(b.ToString("x2"));
                }
            }

            return builder.ToString();
        }

        private static readonly byte[] Key = { 247, 4, 240, 244, 11, 128, 189, 70, 69, 204, 74, 153, 229, 42, 97, 152, 145, 90, 156, 146, 199, 64, 50, 69, 240, 133, 133, 159, 220, 229, 124, 191 };

        private static readonly byte[] Vector = { 153, 229, 42, 97, 152, 145, 90, 156, 146, 199, 64, 50, 69, 85, 185, 97 };

        public static string Encrypt(string text)
        {
            byte[] bytes = EncryptStringToByte(text, Key, Vector);

            return Convert.ToBase64String(bytes);
        }

        public static string Decrypt(string text)
        {
            byte[] bytes = Convert.FromBase64String(text);

            return DecryptStringFromBytes(bytes, Key, Vector);
        }

        public static byte[] EncryptStringToByte(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                // ReSharper disable once PossibleNullReferenceException
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {

            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException(nameof(cipherText));
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                // ReSharper disable once PossibleNullReferenceException
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.

                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }

        public static string GetUrlStringFromBase64(string base64String)
        {
            return base64String.TrimEnd(Padding).Replace('+', '-').Replace('/', '_');
        }

        public static string GetBase64FromUrl(string base64String)
        {
            string incoming = base64String.Replace('_', '/').Replace('-', '+');

            switch (base64String.Length % 4)
            {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }

            return incoming;
        }

        private static readonly char[] Padding = { '=' };

        public static bool IsSequentialDigits(this int[] array)
        {
            return array.Zip(array.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSameDigits(this int[] array)
        {
            return array.Zip(array.Skip(1), (a, b) => a == b).All(x => x);
        }
    }
}
