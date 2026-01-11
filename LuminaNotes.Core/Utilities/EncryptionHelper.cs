using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LuminaNotes.Core.Utilities;

/// <summary>
/// Provides AES-256 encryption/decryption for note content
/// </summary>
public static class EncryptionHelper
{
    private const int KeySize = 256;
    private const int IvSize = 128;
    private const int Iterations = 10000;

    /// <summary>
    /// Encrypt text using AES-256 with password-based key derivation
    /// </summary>
    public static string Encrypt(string plainText, string password)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        var salt = GenerateRandomBytes(32);
        var iv = GenerateRandomBytes(16);

        using var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        using var aes = Aes.Create();
        aes.KeySize = KeySize;
        aes.Key = key.GetBytes(KeySize / 8);
        aes.IV = iv;

        using var encryptor = aes.CreateEncryptor();
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var writer = new StreamWriter(cs))
        {
            writer.Write(plainText);
        }

        var encrypted = ms.ToArray();
        var result = new byte[salt.Length + iv.Length + encrypted.Length];
        Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
        Buffer.BlockCopy(iv, 0, result, salt.Length, iv.Length);
        Buffer.BlockCopy(encrypted, 0, result, salt.Length + iv.Length, encrypted.Length);

        return Convert.ToBase64String(result);
    }

    /// <summary>
    /// Decrypt AES-256 encrypted text
    /// </summary>
    public static string Decrypt(string encryptedText, string password)
    {
        if (string.IsNullOrEmpty(encryptedText))
            return encryptedText;

        try
        {
            var fullData = Convert.FromBase64String(encryptedText);
            var salt = new byte[32];
            var iv = new byte[16];
            var encrypted = new byte[fullData.Length - salt.Length - iv.Length];

            Buffer.BlockCopy(fullData, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(fullData, salt.Length, iv, 0, iv.Length);
            Buffer.BlockCopy(fullData, salt.Length + iv.Length, encrypted, 0, encrypted.Length);

            using var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            using var aes = Aes.Create();
            aes.KeySize = KeySize;
            aes.Key = key.GetBytes(KeySize / 8);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(encrypted);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }
        catch (CryptographicException)
        {
            throw new InvalidOperationException("Incorrect password or corrupted data.");
        }
    }

    /// <summary>
    /// Generate cryptographically secure random bytes
    /// </summary>
    private static byte[] GenerateRandomBytes(int size)
    {
        var bytes = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return bytes;
    }

    /// <summary>
    /// Hash password for storage (NOT for encryption, just verification)
    /// </summary>
    public static string HashPassword(string password)
    {
        var salt = GenerateRandomBytes(32);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(32);

        var result = new byte[salt.Length + hash.Length];
        Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
        Buffer.BlockCopy(hash, 0, result, salt.Length, hash.Length);

        return Convert.ToBase64String(result);
    }

    /// <summary>
    /// Verify password against hash
    /// </summary>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            var data = Convert.FromBase64String(hashedPassword);
            var salt = new byte[32];
            var hash = new byte[32];

            Buffer.BlockCopy(data, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(data, salt.Length, hash, 0, hash.Length);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var testHash = pbkdf2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(hash, testHash);
        }
        catch
        {
            return false;
        }
    }
}
