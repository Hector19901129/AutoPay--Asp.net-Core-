using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoPay.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace AutoPay.Services
{
    public class CryptographyService : ICryptographyService
    {
        private readonly IConfigurationSection _aesConfiguration;

        public CryptographyService(IConfiguration configuration)
        {
            _aesConfiguration = configuration.GetSection("Cryptography");
        }

        public string Encrypt(string plainText, string salt)
        {
            using (var csp = Aes.Create())
            {
                var e = GetCryptoTransform(csp, true, salt);
                var inputBuffer = Encoding.UTF8.GetBytes(plainText);
                var output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                var encrypted = Convert.ToBase64String(output);
                return encrypted;
            }
        }

        public string Decrypt(string cipherText, string salt)
        {
            using (var csp = Aes.Create())
            {
                var d = GetCryptoTransform(csp, false, salt);
                var output = Convert.FromBase64String(cipherText);
                var decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);
                var decypted = Encoding.UTF8.GetString(decryptedOutput);
                return decypted;
            }
        }

        public void Encrypt<T>(T source, string salt, params string[] excludes)
        {
            if (source == null) return;
            //get properties
            var properties = typeof(T).GetProperties();
            //process each properties
            foreach (var property in properties)
            {
                //skip if excluded
                if (excludes != null && excludes.Any(x => x.Equals(property.Name))) continue;
                //get property value
                var val = property.GetValue(source, null)?.ToString();
                //ignore if value is null
                if (string.IsNullOrEmpty(val)) continue;
                //encrypt data
                val = Encrypt(val, salt);
                //update object value
                property.SetValue(source, val);
            }
        }
        public void Encrypt<T>(List<T> sourceList, string salt, params string[] excludes)
        {
            if (sourceList == null || !sourceList.Any())
                return;

            foreach (var source in sourceList)
            {
                Encrypt(source, salt, excludes);
            }
        }


        public void Decrypt<T>(T source, string salt, params string[] excludes)
        {
            if (source == null) return;
            //get properties
            var properties = typeof(T).GetProperties();
            //process each properties
            foreach (var property in properties)
            {
                //skip if excluded
                if (excludes != null && excludes.Any(x => x.Equals(property.Name))) continue;
                //get property value
                var val = property.GetValue(source, null)?.ToString();
                //ignore if value is null
                if (string.IsNullOrEmpty(val)) continue;
                //encrypt data
                val = Decrypt(val, salt);
                //update object value
                property.SetValue(source, val);
            }
        }

        public void Decrypt<T>(List<T> sourceList, string salt, params string[] excludes)
        {
            if (sourceList == null || !sourceList.Any())
                return;

            foreach (var source in sourceList)
            {
                Decrypt(source, salt, excludes);
            }
        }

        #region  private methods

        private ICryptoTransform GetCryptoTransform(SymmetricAlgorithm csp, bool encrypting, string salt)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(_aesConfiguration["PassPhrase"]), Encoding.UTF8.GetBytes(_aesConfiguration["Secret"] + salt), 1024);
            var key = spec.GetBytes(16);
            csp.IV = Encoding.UTF8.GetBytes(_aesConfiguration["InitVector"]);
            csp.Key = key;
            return encrypting ? csp.CreateEncryptor() : csp.CreateDecryptor();
        }

        #endregion
    }
}
