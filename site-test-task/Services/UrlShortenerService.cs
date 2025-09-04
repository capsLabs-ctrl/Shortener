
using System.Security.Cryptography;
using System.Text;
namespace site_test_task.Services
{
    public class UrlShortenerService
    {
        public const string alph = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

        public string GenerateShortCode(int length = 8)
        {
            var bytes = new byte[length];
            rng.GetBytes(bytes);

            var sb = new StringBuilder(length);
            foreach (var b in bytes)
            {
                sb.Append(alph[b % alph.Length]);
            }
            return sb.ToString();
        }
    }
}