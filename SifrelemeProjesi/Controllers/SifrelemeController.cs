using Microsoft.AspNetCore.Mvc;
using SifrelemeProjesi.Models;

namespace SifrelemeProjesi.Controllers
{
    public class SifrelemeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sifrele(string metin, string anahtar)
        {
            SifrelemeModel model = new SifrelemeModel();
            string sifreliMetin = model.Sifrele(metin, anahtar);
            if (sifreliMetin.StartsWith("Metin boş") || sifreliMetin.StartsWith("Anahtar boş") || sifreliMetin.StartsWith("Şifreleme sırasında"))
            {
                ViewBag.Hata = sifreliMetin;
            }
            else
            {
                ViewBag.SifreliMetin = sifreliMetin;
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult SifreCoz(string sifreliMetin, string anahtar)
        {
            SifrelemeModel model = new SifrelemeModel();
            string cozulmusMetin = model.SifreCoz(sifreliMetin, anahtar);
            if (cozulmusMetin.StartsWith("Şifreli metin") || cozulmusMetin.StartsWith("Anahtar boş") || cozulmusMetin.StartsWith("Şifre çözülürken"))
            {
                ViewBag.Hata = cozulmusMetin;
            }
            else
            {
                ViewBag.CozulmusMetin = cozulmusMetin;
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult OzetOlustur(string veri)
        {
            SifrelemeModel model = new SifrelemeModel();
            string ozet = model.OzetOlustur(veri);
            if (ozet.StartsWith("Veri boş") || ozet.StartsWith("Özet oluşturulurken"))
            {
                ViewBag.Hata = ozet;
            }
            else
            {
                ViewBag.Ozet = ozet;
            }
            return View("Index");
        }
    }
}