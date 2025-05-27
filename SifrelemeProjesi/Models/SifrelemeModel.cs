using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SifrelemeProjesi.Models
{
    public class SifrelemeModel
    {
        public string Sifrele(string metin, string anahtar)
        {
            if (string.IsNullOrEmpty(metin))
            {
                return "Metin boş olamaz.";
            }
            if (string.IsNullOrEmpty(anahtar))
            {
                return "Anahtar boş olamaz.";
            }

            byte[] iv = new byte[16];
            byte[] array;

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(anahtar.PadRight(32).Substring(0, 32));
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(metin);
                            }
                            array = memoryStream.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(array);
            }
            catch (Exception ex)
            {
                return "Şifreleme sırasında hata oluştu: " + ex.Message;
            }
        }

        public string SifreCoz(string sifreliMetin, string anahtar)
        {
            if (string.IsNullOrEmpty(sifreliMetin))
            {
                return "Şifreli metin boş olamaz.";
            }
            if (string.IsNullOrEmpty(anahtar))
            {
                return "Anahtar boş olamaz.";
            }

            if (!IsBase64String(sifreliMetin))
            {
                return "Şifreli metin geçerli bir Base64 stringi değil.";
            }

            byte[] iv = new byte[16];
            byte[] buffer;

            try
            {
                buffer = Convert.FromBase64String(sifreliMetin);
            }
            catch (FormatException)
            {
                return "Şifreli metin geçerli bir Base64 stringi değil.";
            }

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(anahtar.PadRight(32).Substring(0, 32));
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Şifre çözülürken hata oluştu: " + ex.Message;
            }
        }

        public string OzetOlustur(string veri)
        {
            if (string.IsNullOrEmpty(veri))
            {
                return "Veri boş olamaz.";
            }

            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(veri));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                return "Özet oluşturulurken hata oluştu: " + ex.Message;
            }
        }

        private bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }
}