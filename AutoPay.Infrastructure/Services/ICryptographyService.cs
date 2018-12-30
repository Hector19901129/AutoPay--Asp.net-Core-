using System.Collections.Generic;

namespace AutoPay.Infrastructure.Services
{
    public interface ICryptographyService
    {
        string Encrypt(string raw, string salt);
        void Encrypt<T>(T source, string salt, params string[] excludes);
        void Encrypt<T>(List<T> sourceList, string salt, params string[] excludes);
        string Decrypt(string encrypted, string salt);
        void Decrypt<T>(T source, string salt, params string[] excludes);
        void Decrypt<T>(List<T> sourceList, string salt, params string[] excludes);
    }
}
