using System;
using System.Security.Cryptography;
using System.Text;

namespace KriptografiFinalProje.Services
{
    public class SHAmetin
    {
        public string MetinOzetiCikar(string metin)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(metin);
                    byte[] hash = sha256.ComputeHash(bytes);
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }
    }
} 