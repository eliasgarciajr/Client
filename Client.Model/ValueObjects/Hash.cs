using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Client.Model.ValueObjects
{
    public class Hash
    {
        private HashAlgorithm _hash;

        public Hash(HashAlgorithm hash)
        {
            _hash = hash;
        }

        public string Encrypt(string password)
        {
            var encodedValue = Encoding.UTF8.GetBytes(password);
            var encryptedPassword = _hash.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool Verify(string passwordNow, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new NullReferenceException("Cadastre uma senha.");

            var encryptedPassword = _hash.ComputeHash(Encoding.UTF8.GetBytes(passwordNow));

            var sb = new StringBuilder();

            foreach (var caractere in encryptedPassword)
            {
                sb.Append(caractere.ToString("X2"));
            }

            return sb.ToString() == password;
        }
    }
}
