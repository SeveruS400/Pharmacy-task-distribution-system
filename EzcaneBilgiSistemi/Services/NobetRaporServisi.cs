using EzcaneBilgiSistemi.Models;
using Repositories;
using Repositories.Contracts;

namespace EzcaneBilgiSistemi.Services
{
    public class NobetRaporServisi
    {
        private readonly INobetlerRepository _nobetlerRepository;
        public NobetRaporServisi(INobetlerRepository nobetlerRepository)
        {
            _nobetlerRepository = nobetlerRepository;
        }

        public List<NobetStatisticsViewModel> GetNobetRapor()
        {
            var nobetRapor = new List<NobetStatisticsViewModel>();

            var nobetler = _nobetlerRepository.GetAllNobetlerTypeOfList();

            // Eczaneleri grupla
            var grupluEczaneler = nobetler.GroupBy(n => n.EczaneBilgileriId);

            foreach (var eczaneGrubu in grupluEczaneler)
            {
                var eczane = eczaneGrubu.First().EczaneBilgileri;
                var rapor = new NobetStatisticsViewModel
                {
                    EczaneAdi = eczane.EczaneAdi,
                    ResmiTatilSayisi = eczaneGrubu.Count(n => n.NobetTuru == "Resmi Tatil"),
                    CumartesiSayisi = eczaneGrubu.Count(n => n.NobetTuru == "Cumartesi"),
                    PazarSayisi = eczaneGrubu.Count(n => n.NobetTuru == "Pazar"),
                    HaftaiciSayisi = eczaneGrubu.Count(n => n.NobetTuru == "Haftaiçi")
                };

                nobetRapor.Add(rapor);
            }

            return nobetRapor;
        }
    }
}
