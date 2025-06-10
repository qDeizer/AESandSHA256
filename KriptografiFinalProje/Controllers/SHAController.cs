using Microsoft.AspNetCore.Mvc;
using KriptografiFinalProje.Services;

namespace KriptografiFinalProje.Controllers
{
    public class SHAController : Controller
    {
        private readonly SHAmetin _shaMetin;
        private readonly SHAdosya _shaDosya;

        public SHAController()
        {
            _shaMetin = new SHAmetin();
            _shaDosya = new SHAdosya();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MetinOzetiCikar(string metin)
        {
            var ozet = _shaMetin.MetinOzetiCikar(metin);
            return Json(new { ozet });
        }

        [HttpPost]
        public IActionResult DosyaOzetiCikar(IFormFile dosya)
        {
            if (dosya == null || dosya.Length == 0)
                return Json(new { ozet = "Dosya seçilmedi veya boş!" });

            using (var stream = dosya.OpenReadStream())
            {
                var ozet = _shaDosya.DosyaOzetiCikar(stream);
                return Json(new { ozet });
            }
        }
    }
}