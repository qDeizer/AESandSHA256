using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KriptografiFinalProje.Services
{
    public class AEScoz
    {
        public string MetniCoz(string sifreliMetin, string sifre)
        {
            try
            {
                byte[] sifreliMetinBytes = Convert.FromBase64String(sifreliMetin);
                byte[] sifreBytes = Encoding.UTF8.GetBytes(sifre);

                using (Aes aes = Aes.Create())
                {
                    // Şifreyi 32 byte'a tamamla (SHA256 ile)
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        sifreBytes = sha256.ComputeHash(sifreBytes);
                        aes.Key = sifreBytes;
                    }

                    // IV'yi al (ilk 16 byte)
                    byte[] iv = new byte[16];
                    Array.Copy(sifreliMetinBytes, 0, iv, 0, 16);
                    aes.IV = iv;

                    // Şifreli metni çöz
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(sifreliMetinBytes, 16, sifreliMetinBytes.Length - 16);
                            cs.FlushFinalBlock();
                        }

                        byte[] cozulmusMetinBytes = ms.ToArray();
                        return Encoding.UTF8.GetString(cozulmusMetinBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }
    }
} 