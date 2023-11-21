using Entities.Models;
using EzcaneBilgiSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace EzcaneBilgiSistemi.Controllers
{
    public class OptimumMesafeController : Controller
    {
        #region Ctor
        private readonly IRepositoryManager _manager;

        public OptimumMesafeController(IRepositoryManager manager)
        {
            _manager = manager;
        }
        #endregion
        public IActionResult Index()
        {
            var eczanelist = _manager.EzcaneBilgileri.GetAll(false).Select(s => new EczaneSelectItem
            {
                Value = s.Id.ToString(),
                Text = s.EczaneAdi,
                Longitude = s.EczaneKordinatLongitude,
                Latitude = s.EczaneKordinatLatitude
            }).ToList();
            ViewBag.Eczaneler = eczanelist;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetEczanelerInRange(double merkezLatitude, double merkezLongitude, double cap)
        {
            try
            {
                var eczanelerInRange = new List<EczaneBilgileri>();

                var eczaneler = _manager.EzcaneBilgileri.GetAll(false);
                foreach (var eczane in eczaneler)
                {
                    var mesafe = MesafeHesaplayici.Haversine(merkezLatitude, merkezLongitude, eczane.EczaneKordinatLatitude, eczane.EczaneKordinatLongitude);
                    if (mesafe <= cap && mesafe != 0)
                    {
                        eczanelerInRange.Add(eczane);
                    }
                }

                var eczanelist = _manager.EzcaneBilgileri.GetAll(false).Select(s => new EczaneSelectItem
                {
                    Value = s.Id.ToString(),
                    Text = s.EczaneAdi,
                    Longitude = s.EczaneKordinatLongitude,
                    Latitude = s.EczaneKordinatLatitude
                }).ToList();
                ViewBag.Eczaneler = eczanelist;
                return PartialView("_OptimumTablo", eczanelerInRange);
            }
            catch (Exception ex)
            {
                // Hata detayını logla
                Console.WriteLine(ex.Message);
                throw; // Hatanın tekrar fırlatılması, daha fazla inceleme için
            }
        }

    }
    public class MesafeHesaplayici
    {
        public static double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Dünya yarıçapı in kilometers

            var dLat = Deg2Rad(lat2 - lat1);
            var dLon = Deg2Rad(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = R * c; // Mesafe in kilometers
            return distance;
        }

        private static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
