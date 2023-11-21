using Entities.Models;
using EzcaneBilgiSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repositories.Contracts;

namespace EzcaneBilgiSistemi.Controllers
{
    public class GecmisNobetlerController : Controller
    {
        private readonly INobetlerRepository _nobetlerRepository;
        private readonly UygulamaAyarları _uygulamaAyarları;
        private readonly IRepositoryManager _repositoryManager;
        public GecmisNobetlerController(INobetlerRepository nobetlerRepository,
            IOptionsMonitor<UygulamaAyarları> optionsMonitor,
            IRepositoryManager repositoryManager)
        {
            _nobetlerRepository = nobetlerRepository;
            _uygulamaAyarları = optionsMonitor.CurrentValue;
            _repositoryManager = repositoryManager;
        }
        public IActionResult Index()
        {
            var gecmisNobetler = _nobetlerRepository.GetAllNobetlerTypeOfList();
            var nobetGruplari = GecmisNobetleriGrupla(gecmisNobetler);
            return View(nobetGruplari);
        }
        private List<NobetGrupViewModel> GecmisNobetleriGrupla(List<Nobetler> nobetler)
        {
            var nobetGruplari = new List<NobetGrupViewModel>();

            var tarihler = nobetler.Select(n => n.Tarih.Date).Distinct().OrderBy(d => d).ToList();

            foreach (var tarih in tarihler)
            {
                var nobetlerTarihli = nobetler.Where(n => n.Tarih.Date == tarih).ToList();

                var nobetGrup = new NobetGrupViewModel
                {
                    Tarih = tarih,
                    NobetTuru = string.Empty,
                    EczaneAdlari = new List<string>()
                };

                foreach (var nobet in nobetlerTarihli.GroupBy(n => n.NobetTuru))
                {
                    nobetGrup.NobetTuru = nobet.Key;

                    foreach (var eczane in nobet.Select(n => n.EczaneBilgileri.EczaneAdi))
                    {
                        if (!nobetGrup.EczaneAdlari.Contains(eczane))
                        {
                            nobetGrup.EczaneAdlari.Add(eczane);
                        }
                    }
                }
                nobetGruplari.Add(nobetGrup);
            }
            var enBuyukEczaneAdet = Math.Max(_uygulamaAyarları.HaftaiciNS, Math.Max(_uygulamaAyarları.CumartesiNS, Math.Max(_uygulamaAyarları.PazarNS, _uygulamaAyarları.ResmiNS)));

            ViewBag.EczaneAdet = enBuyukEczaneAdet;
            return nobetGruplari;
        }

        public async Task<IActionResult> NobetSil()
        {
            await _repositoryManager.Nobetler.DeleteAllAsync();
            return RedirectToAction("Index");
        }
    }
}
