using System;
using System.Security.Cryptography;

namespace Management.Api.Services
{
    /// <summary>
    ///     Implements hashing methods, see https://github.com/aspnet/AspNetIdentity/blob/master/src/Microsoft.AspNet.Identity.Core/Crypto.cs
    /// </summary>
    public class PasswordHasher
    {
        private const int PBKDF2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int PBKDF2SubkeyLength = 256 / 8; // 256 bits
        private const int SaltSize = 256 / 8; // 128 bits

        public PasswordHasher()
        {

        }

        /// <summary>
        ///     Hash a password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            byte[] salt;
            byte[] subkey;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        ///     Verify that a password matches the hashedPassword
        /// </summary>
        /// <param name="hashedKey"></param>
        /// <param name="providedKey"></param>
        /// <returns></returns> 
        public bool VerifyHashedPassword(string hashedKey, string providedKey)
        {
            if (hashedKey == null)
            {
                return false;
            }
            if (providedKey == null)
            {
                throw new ArgumentNullException("password");
            }

            var hashedKeyBytes = Convert.FromBase64String(hashedKey);


            if (hashedKeyBytes.Length != (1 + SaltSize + PBKDF2SubkeyLength) || hashedKeyBytes[0] != 0x00)
            {
                // Wrong length or version header.
                return false;
            }

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedKeyBytes, 1, salt, 0, SaltSize);
            var storedSubkey = new byte[PBKDF2SubkeyLength];
            Buffer.BlockCopy(hashedKeyBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(providedKey, salt, PBKDF2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}