![image](https://github.com/user-attachments/assets/baa236e6-e9f7-466e-8a82-b7d232aabc99)
![image](https://github.com/user-attachments/assets/a0af224b-eb8c-4616-8e2e-b923a9372977)
![image](https://github.com/user-attachments/assets/031cf5ab-eeff-4327-b1bb-691debfa383e)
![image](https://github.com/user-attachments/assets/1e36d05d-ecb9-41a0-bb42-9734f59899fb)

Projeyi Çalıştırma:
►proje bilgisayara indirelecek.(zip olarak indirilecek)

►zip ise zipden çıkartılacak.

►visual studio açılacak.

►"open project/solution" a tıklanacak.

►zipden çıkarılan dosyanın içerisinden "KriptografiFinalProje.sln" dosyası seçilecek.

►projeye güven denilecek.

►proje başlatılacak (f5).

►proje otomatik olarak bir tarayıcıda başlatılır.

►Başlatılmazsa visual studionun içinden output sekmesinde projenin başladığı "https://localhost:XXXX" adresine tıklanacak.

projenin 1dk lık video sunumu:
https://youtu.be/tcRXGoKPNk4  


Şifreleme ve Özet Çıkarma: Mantıksal ve Matematiksel Açıklama
1. SHA-256 Özet Çıkarma (Hashing)
Ne Yapar?

SHA-256, bir metni veya dosyayı alır ve sabit uzunlukta (64 karakter, 256 bit) bir "özet" üretir. Bu özet, verinin benzersiz bir parmak izidir. Aynı veri hep aynı özeti verir, ama özetten veriye geri dönülemez.

Mantıksal Süreç:

Kullanıcı bir metin (örneğin, "Merhaba") veya dosya yükler.
Sistem, veriyi baytlara çevirir (örneğin, UTF-8 ile).
SHA-256 algoritması şu adımlardan geçer:
Veriyi Bölme (Padding): Veriyi 512 bitlik parçalara ayırır. Son parçaya, verinin uzunluğu eklenir.
Başlangıç Değerleri: 8 tane sabit sayı (hash başlangıç değerleri) ve 64 tane sabit (round sabitleri) kullanılır.
Sıkıştırma: Her 512 bitlik parça için:
Bitwise İşlemler: AND, OR, XOR gibi mantıksal işlemler.
Dönüşümler: Baytları kaydırma (shift) ve döndürme (rotate).
Modüler Toplama: Sayılar, belirli bir sınır içinde toplanır (mod 2³²).
64 tur boyunca bu işlemler tekrarlanır.
Sonuç: 256 bitlik bir özet (hexadecimal olarak 64 karakter) üretilir.
Örnek: "Merhaba" → a591a6d40bf420404a011733cfb7b190d62c65bf0bcda32b57b277d9ad9f146e.
Dosya için Fark: Dosya, bir akış (stream) olarak okunur ve aynı SHA-256 işlemleri uygulanır. Büyük dosyalar parça parça işlenir.

Neden Güvenli?

Tek Yönlü: Özetten orijinal veriye dönülemez.
Çarpışma Direnci: Farklı verilerin aynı özeti üretmesi neredeyse imkânsız.
Hızlı: Büyük verilerde bile hızlı çalışır.
2. AES Şifreleme ve Şifre Çözme
Ne Yapar?

AES, metni bir anahtar (şifre) kullanarak şifreler veya çözer. Aynı anahtar, hem şifreleme hem de çözme için kullanılır (simetrik şifreleme). Proje, AES-256 (256 bit anahtar) ve CBC (Cipher Block Chaining) modunu kullanır.

Şifreleme Süreci
Mantıksal Adımlar:

Giriş:
Kullanıcı metin (örneğin, "Gizli Mesaj") ve şifre (örneğin, "sifre123") girer.
Anahtar Türetme:
Şifre, SHA-256 ile işlenerek 32 baytlık (256 bit) bir anahtar olur. Bu, farklı uzunluktaki şifreleri standart bir uzunluğa getirir.
IV (Initialization Vector):
Rastgele bir 16 baytlık IV üretilir. Bu, aynı metni aynı anahtarla şifrelesen bile farklı sonuçlar çıkmasını sağlar (güvenlik için).
Şifreleme:
Metin, 128 bitlik bloklara bölünür.
Her blok için AES algoritması çalışır:
SubBytes: Her bayt, bir S-box tablosuyla değiştirilir (karıştırma).
ShiftRows: Bloktaki satırlar kaydırılır.
MixColumns: Sütunlar matematiksel olarak karıştırılır (polinom çarpımı).
AddRoundKey: Anahtarla XOR yapılır.
CBC Modu: Her blok, önceki şifreli blokla XOR yapılır (ilk blok IV ile).
14 tur boyunca bu işlemler tekrarlanır (AES-256 için).
Çıktı:
Şifreli metin (IV + şifreli veri), Base64 formatına çevrilir (örneğin, U2FsdGVkX1+...).
Base64, binary veriyi metne çevirir, böylece kolayca saklanabilir/gösterilir.
Örnek:

Metin: "Gizli Mesaj"
Şifre: "sifre123"
Çıktı: Base64 formatında bir şifreli metin.
Şifre Çözme Süreci
Mantıksal Adımlar:

Giriş:
Kullanıcı, şifreli metni (Base64 formatında) ve şifreyi girer.
Anahtar Türetme:
Şifre, SHA-256 ile 32 baytlık anahtara çevrilir (şifrelemedekiyle aynı).
IV Ayıklama:
Şifreli metnin ilk 16 baytı IV olarak alınır.
Şifre Çözme:
Kalan baytlar, AES’in ters işlemleriyle çözülür:
InvSubBytes: Ters S-box dönüşümü.
InvShiftRows: Ters satır kaydırma.
InvMixColumns: Ters sütun karıştırma.
AddRoundKey: Anahtarla XOR.
CBC modunda, her blok önceki şifreli blokla işlenir.
Çıktı:
Çözülen metin, UTF-8 ile okunabilir metne çevrilir (örneğin, "Gizli Mesaj").
Yanlış şifre veya geçersiz metin varsa hata döner.
Neden Güvenli?

Simetrik Anahtar: Aynı anahtar, şifreleme ve çözme için kullanılır, bu yüzden anahtar gizli tutulmalıdır.
CBC Modu: IV ile birlikte, aynı metnin farklı şifrelenmiş halleri olur.
256 Bit Anahtar: Kaba kuvvet saldırılarına karşı çok dayanıklı.
