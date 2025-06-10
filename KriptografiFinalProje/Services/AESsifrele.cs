using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KriptografiFinalProje.Services
{
    public class AESsifrele
    {
        public string MetniSifrele(string metin, string sifre)
        {
            try
            {
                byte[] sifreBytes = Encoding.UTF8.GetBytes(sifre);
                byte[] metinBytes = Encoding.UTF8.GetBytes(metin);

                using (Aes aes = Aes.Create())
                {
                    // Şifreyi 32 byte'a tamamla (SHA256 ile)
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        sifreBytes = sha256.ComputeHash(sifreBytes);
                        aes.Key = sifreBytes;
                    }

                    aes.GenerateIV(); // Rastgele IV oluştur
                    byte[] sifreliMetin;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Önce IV'yi yaz
                        ms.Write(aes.IV, 0, aes.IV.Length);

                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(metinBytes, 0, metinBytes.Length);
                            cs.FlushFinalBlock();
                        }

                        sifreliMetin = ms.ToArray();
                    }

                    return Convert.ToBase64String(sifreliMetin);
                }
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }
    }
} 