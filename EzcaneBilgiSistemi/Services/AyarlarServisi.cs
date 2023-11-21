
using EzcaneBilgiSistemi.Models;
using Microsoft.Extensions.Options;

namespace EzcaneBilgiSistemi.Settings
{
    public class AyarlarServisi
    {
        private readonly IOptionsMonitor<UygulamaAyarları> _optionsMonitor;

        public AyarlarServisi(IOptionsMonitor<UygulamaAyarları> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;

            // Ayarlar değiştiğinde tetiklenecek olaya bir dinleyici ekleyin
            _optionsMonitor.OnChange((newAyarlar, changeToken) => AyarlariGuncelle(newAyarlar));
        }

        public UygulamaAyarları GetAyarlar()
        {
            return _optionsMonitor.CurrentValue;
        }

        public void AyarlariGuncelle(UygulamaAyarları yeniAyarlar)
        {
            // Yeni ayarları güncelleme işlemi
            _optionsMonitor.CurrentValue.EczaneAramaCap = yeniAyarlar.EczaneAramaCap;
            _optionsMonitor.CurrentValue.HaftaiciNS = yeniAyarlar.HaftaiciNS;
            _optionsMonitor.CurrentValue.CumartesiNS = yeniAyarlar.CumartesiNS;
            _optionsMonitor.CurrentValue.PazarNS = yeniAyarlar.PazarNS;
            _optionsMonitor.CurrentValue.ResmiNS = yeniAyarlar.ResmiNS;
            _optionsMonitor.CurrentValue.NobetOSDinlenme = yeniAyarlar.NobetOSDinlenme;
            _optionsMonitor.CurrentValue.AyniNobetTuruKarsilasmasin = yeniAyarlar.AyniNobetTuruKarsilasmasin;
        }
    }
}





