using System;
using System.Security.Cryptography;
using Code9Insta.API.Helpers.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Code9Insta.API.Helpers
{
    public class PasswordManager : IPasswordManager
    {
        public string GetPasswordHash(string password, byte[] salt)
        {
            var hashed = string.Empty;

                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: 10000,
               numBytesRequested: 256 / 8));
            

            return hashed;
        }
    }
}
