using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LuminaNotes.Core.Utilities;

/// <summary>
/// Provides AES-256 encryption/decryption for sensitive note content
/// </summary>
public static class EncryptionHelper
{
    private const int KeySize = 256;
    private const int BlockSize = 128;

    /// <summary>
    /// Encrypts text using AES-256
    /// </summary>
    public static string Encrypt(string plainText, string password)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        byte[] encrypted;
        byte[] salt = GenerateSalt();

        using (var aes = Aes.Create())
        {
            aes.KeySize = KeySize;
            aes.BlockSize = BlockSize;

            var key = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            encrypted = ms.ToArray();
        }

        // Prepend salt to encrypted data
        var result = new byte[salt.Length + encrypted.Length];
        Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
        Buffer.BlockCopy(encrypted, 0, result, salt.Length, encrypted.Length);

        return Convert.ToBase64String(result);
    }

    /// <summary>
    /// Decrypts AES-256 encrypted text
    /// </summary>
    public static string Decrypt(string cipherText, string password)
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        try
        {
            var fullCipher = Convert.FromBase64String(cipherText);
            var salt = new byte[32];
            var cipher = new byte[fullCipher.Length - salt.Length];

            Buffer.BlockCopy(fullCipher, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(fullCipher, salt.Length, cipher, 0, cipher.Length);

            using var aes = Aes.Create();
            aes.KeySize = KeySize;
            aes.BlockSize = BlockSize;

            var key = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        catch (Exception ex)
        {
            throw new CryptographicException("Decryption failed. Invalid password or corrupted data.", ex);
        }
    }

    private static byte[] GenerateSalt()
    {
        var salt = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }
}
