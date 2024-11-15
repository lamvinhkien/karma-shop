using System.Security.Cryptography;
using System.Text;

namespace WebApp.Services;

public static class Helper
{
    public static string Hash(string plainText)
    {
        using HashAlgorithm algorithm = SHA512.Create();
        return Convert.ToHexString(algorithm.ComputeHash(Encoding.ASCII.GetBytes(plainText)));
    }

    public static string HmacSha512(string plainText, string key)
    {
        using (HashAlgorithm algorithm = new HMACSHA512(Encoding.ASCII.GetBytes(key)))
        {
            return Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
        }
    }
}