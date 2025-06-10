using Microsoft.AspNetCore.Mvc;
using KriptografiFinalProje.Services;

namespace KriptografiFinalProje.Controllers
{
    public class AESController : Controller
    {
        private readonly AESsifrele _aessifrele;
        private readonly AEScoz _aescoz;

        public AESController()
        {
            _aessifrele = new AESsifrele();
            _aescoz = new AEScoz();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sifrele(string metin, string sifre)
        {
            if (string.IsNullOrEmpty(metin) || string.IsNullOrEmpty(sifre))
                return Json(new { sifreliMetin = "Metin ve şifre boş olamaz!" });

            var sifreliMetin = _aessifrele.MetniSifrele(metin, sifre);
            return Json(new { sifreliMetin });
        }

        [HttpPost]
        public IActionResult Coz(string sifreliMetin, string sifre)
        {
            if (string.IsNullOrEmpty(sifreliMetin) || string.IsNullOrEmpty(sifre))
                return Json(new { cozulmusMetin = "Şifreli metin ve şifre boş olamaz!" });

            var cozulmusMetin = _aescoz.MetniCoz(sifreliMetin, sifre);
            return Json(new { cozulmusMetin });
        }
    }
} 