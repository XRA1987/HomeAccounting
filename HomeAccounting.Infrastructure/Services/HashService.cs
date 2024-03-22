using HomeAccounting.Application.Abstractions;
using System.Text;
using XSystem.Security.Cryptography;

namespace HomeAccounting.Infrastructure.Services
{
    public class HashService : IHashService
    {
        public string GetHash(string value)
        {
            var sha256 = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
