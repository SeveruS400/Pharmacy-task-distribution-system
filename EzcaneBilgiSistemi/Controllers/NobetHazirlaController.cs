using Entities.Models;
using EzcaneBilgiSistemi.Models;
using EzcaneBilgiSistemi.Services;
using EzcaneBilgiSistemi.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repositories;
using Repositories.Contracts;
using Repositories.Implementations;

namespace EzcaneBilgiSistemi.Controllers
{
    public class NobetHazirlaController : Controller
    {
        #region Ctor
        private readonly IEczaneBilgileriRepository _eczaneBilgileriRepository;
        private readonly IResmiTatillerRepository _resmiTatillerRepository;
        private readonly UygulamaAyarları _uygulamaAyarları;
        private readonly INobetlerRepository _nobetRepository;
        private readonly IRepositoryManager _manager;
        private readonly INobetDagilimRepository _nobetDagilimiRepository;
        private readonly DataContext _context;

        public NobetHazirlaController(IEczaneBilgileriRepository eczaneBilgileriRepository,
            IResmiTatillerRepository resmiTatillerRepository,
            INobetlerRepository nobetlerRepository,
            IRepositoryManager manager,
            IOptionsMonitor<UygulamaAyarları> optionsMonitor,
            INobetDagilimRepository nobetDagilimRepository,
            DataContext context)
        {
            _eczaneBilgileriRepository = eczaneBilgileriRepository;
            _resmiTatillerRepository = resmiTatillerRepository;
            _nobetRepository = nobetlerRepository;
            _manager = manager;
            _uygulamaAyarları = optionsMonitor.CurrentValue;
            _nobetDagilimiRepository = nobetDagilimRepository;
            _context = context;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost, HttpGet]
        public IActionResult NobetDagit(DateTime startDate, DateTime endDate)
        {
            //#region Viewbag
            //DateTime lastNobetDate = _nobetRepository.GetLastNobetDate();
            //if (startDate >= endDate)
            //{
            //    ViewBag.ErrorMessage = "Başlangıç tarihi, bitiş tarihinden küçük olmalıdır.";
            //    return View("Index");
            //}
            //if (startDate < DateTime.Now)
            //{
            //    ViewBag.ErrorMessage = "Geçmişe yönelik nöbet ataması yapamazsınız.";
            //    return View("Index");

            //}
            //if (lastNobetDate == DateTime.Today)
            //{
            //    if (lastNobetDate > startDate)
            //    {
            //        ViewBag.ErrorMessage = "Seçilen aralıkta atanan nöbet mevcuttur. Lütfen " + lastNobetDate.Date + " tarihinden sonra nöbet oluşturunuz.";
            //        return View("Index");
            //    }
            //}

            //ViewBag.ErrorMessage = "İşlem başarılı.";
            //#endregion

            #region Variables
            var eczaneler = _eczaneBilgileriRepository.GetAllEczanesTypeOfList();
            //var nobetler = _nobetDagilimiRepository.GetAllNobetDagilim();

            DateGroupService dateGroupService = new DateGroupService(_resmiTatillerRepository);
            var groupedDates = dateGroupService.GroupDates(startDate, endDate);
            
            UygulamaAyarları appSet = new UygulamaAyarları
            {
                EczaneAramaCap = _uygulamaAyarları.EczaneAramaCap,
                HaftaiciNS = _uygulamaAyarları.HaftaiciNS,
                CumartesiNS = _uygulamaAyarları.CumartesiNS,
                PazarNS = _uygulamaAyarları.PazarNS,
                ResmiNS = _uygulamaAyarları.ResmiNS,
                NobetOSDinlenme = _uygulamaAyarları.NobetOSDinlenme,
                AyniNobetTuruKarsilasmasin = _uygulamaAyarları.AyniNobetTuruKarsilasmasin
            };
            List<Nobetler> OD = new List<Nobetler>();
            Dictionary<int, int> yasakliEczaneler = new Dictionary<int, int>();
            foreach (var eczane in eczaneler)
            {
                int yasakliEczaneId = _eczaneBilgileriRepository.GetYasakliEczaneById(eczane.Id);
                yasakliEczaneler[eczane.Id] = yasakliEczaneId;
            }
            #endregion

            var nobetAlgoritmasi = new NobetDagilimiServisi(eczaneler, groupedDates, _manager, appSet, OD, _nobetRepository, yasakliEczaneler, _nobetDagilimiRepository);
            nobetAlgoritmasi.NobetleriDagit();

            return View("Index");
        }

    }
}
