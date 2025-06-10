using System;
using System.IO;
using System.Security.Cryptography;

namespace KriptografiFinalProje.Services
{
    public class SHAdosya
    {
        public string DosyaOzetiCikar(Stream dosyaStream)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(dosyaStream);
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