// ... existing code ...

// Form validation fonksiyonları
function formKontrol(element, durum) {
    element.classList.remove('error', 'warning');
    if (durum) {
        element.classList.add(durum);
    }
}

function sonucKutusuKontrol(element, durum, mesaj) {
    element.classList.remove('error', 'warning');
    if (durum) {
        element.classList.add(durum);
    }
    element.textContent = mesaj;
}

// Form temizleme fonksiyonu
function formlariTemizle(formId) {
    const form = document.getElementById(formId);
    if (form) {
        const inputs = form.getElementsByClassName('form-control');
        Array.from(inputs).forEach(input => {
            formKontrol(input, '');
        });
    }
}

// SHA-256 işlemleri
function metinOzetiCikar() {
    const metinInput = document.getElementById('metin');
    const metin = metinInput.value;
    const sonucKutusu = document.getElementById('ozet');

    // Diğer formu temizle
    formlariTemizle('dosyaForm');

    if (!metin) {
        formKontrol(metinInput, 'warning');
        sonucKutusuKontrol(sonucKutusu, 'warning', 'Lütfen metin giriniz!');
        return;
    }

    formKontrol(metinInput, '');
    $.post('/SHA/MetinOzetiCikar', { metin: metin }, function (data) {
        sonucKutusuKontrol(sonucKutusu, '', data.ozet);
    });
}

function dosyaOzetiCikar() {
    const dosyaInput = document.getElementById('dosya');
    const dosya = dosyaInput.files[0];
    const sonucKutusu = document.getElementById('ozet');

    // Diğer formu temizle
    formlariTemizle('metinForm');

    if (!dosya) {
        formKontrol(dosyaInput, 'warning');
        sonucKutusuKontrol(sonucKutusu, 'warning', 'Lütfen dosya seçiniz!');
        return;
    }

    formKontrol(dosyaInput, '');
    const formData = new FormData();
    formData.append('dosya', dosya);

    $.ajax({
        url: '/SHA/DosyaOzetiCikar',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            sonucKutusuKontrol(sonucKutusu, '', data.ozet);
        }
    });
}

// AES işlemleri
function metniSifrele() {
    const metinInput = document.getElementById('sifrelenecekMetin');
    const sifreInput = document.getElementById('sifrelemeAnahtari');
    const metin = metinInput.value;
    const sifre = sifreInput.value;
    const sonucKutusu = document.getElementById('sifreliMetin');

    // Diğer formu temizle
    formlariTemizle('cozmeForm');

    if (!metin || !sifre) {
        if (!metin) formKontrol(metinInput, 'warning');
        if (!sifre) formKontrol(sifreInput, 'warning');
        sonucKutusuKontrol(sonucKutusu, 'warning', 'Lütfen metin ve şifre giriniz!');
        return;
    }

    formKontrol(metinInput, '');
    formKontrol(sifreInput, '');

    $.post('/AES/Sifrele', { metin: metin, sifre: sifre }, function (data) {
        if (data.sifreliMetin.startsWith('Hata oluştu:')) {
            sonucKutusuKontrol(sonucKutusu, 'error', 'Şifreleme işlemi başarısız oldu!');
        } else {
            sonucKutusuKontrol(sonucKutusu, '', data.sifreliMetin);
        }
    });
}

function metniCoz() {
    const metinInput = document.getElementById('cozulecekMetin');
    const sifreInput = document.getElementById('cozmeAnahtari');
    const sifreliMetin = metinInput.value;
    const sifre = sifreInput.value;
    const sonucKutusu = document.getElementById('cozulmusMetin');

    // Diğer formu temizle
    formlariTemizle('sifrelemeForm');

    if (!sifreliMetin || !sifre) {
        if (!sifreliMetin) formKontrol(metinInput, 'warning');
        if (!sifre) formKontrol(sifreInput, 'warning');
        sonucKutusuKontrol(sonucKutusu, 'warning', 'Lütfen şifreli metin ve şifre giriniz!');
        return;
    }

    formKontrol(metinInput, '');
    formKontrol(sifreInput, '');

    $.post('/AES/Coz', { sifreliMetin: sifreliMetin, sifre: sifre }, function (data) {
        if (data.cozulmusMetin.startsWith('Hata oluştu:')) {
            if (data.cozulmusMetin.includes('Base-64')) {
                sonucKutusuKontrol(sonucKutusu, 'error', 'Geçersiz şifreli metin formatı!');
                formKontrol(metinInput, 'error');
            } else {
                sonucKutusuKontrol(sonucKutusu, 'error', 'Şifre çözme işlemi başarısız oldu! Şifre yanlış olabilir.');
                formKontrol(sifreInput, 'error');
            }
        } else {
            sonucKutusuKontrol(sonucKutusu, '', data.cozulmusMetin);
        }
    });
}