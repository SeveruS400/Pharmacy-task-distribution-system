using EzcaneBilgiSistemi.Models;
using EzcaneBilgiSistemi.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EzcaneBilgiSistemi.Controllers
{
    public class AyarlarController : Controller
    {
        private readonly AyarlarServisi _ayarlarServisi;
        private readonly IOptionsSnapshot<UygulamaAyarları> _optionsSnapshot;

        public AyarlarController(AyarlarServisi ayarlarServisi, IOptionsSnapshot<UygulamaAyarları> optionsSnapshot)
        {
            _ayarlarServisi = ayarlarServisi;
            _optionsSnapshot = optionsSnapshot;
        }
        public IActionResult Index()
        {
            var eczaneAramaCap = _ayarlarServisi.GetAyarlar();
            return View(eczaneAramaCap);
        }
        [HttpPost]
        public IActionResult AyarlariGuncelle([FromBody] UygulamaAyarları yeniAyarlar)
        {
            _ayarlarServisi.AyarlariGuncelle(yeniAyarlar);

            return Ok();
        }
    }
}
