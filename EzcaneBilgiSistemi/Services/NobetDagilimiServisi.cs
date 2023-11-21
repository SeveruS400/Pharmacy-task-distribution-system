using Entities.Models;
using EzcaneBilgiSistemi.Models;
using EzcaneBilgiSistemi.Services;
using Repositories;
using Repositories.Contracts;
using Repositories.Implementations;
using System;
using System.Threading.Tasks;

namespace EzcaneBilgiSistemi.Settings
{
    public class NobetDagilimiServisi
    {
        #region Ctor
        private readonly List<EczaneBilgileri> eczaneler;
        private readonly DateGroupService groupedDates;
        //private readonly List<NobetDagilim> nobetler;
        private readonly Dictionary<int, int> yasakliEczaneler;
        private readonly IRepositoryManager _repositoryManager;
        private readonly INobetlerRepository _nobetlerRepository;
        private readonly UygulamaAyarları appSet;
        private readonly List<Nobetler> OD;
        private readonly INobetDagilimRepository _nobetDagilimiRepository;


        public NobetDagilimiServisi(List<EczaneBilgileri> eczaneler,
            DateGroupService groupedDates,
            //List<NobetDagilim> nobetler,
            IRepositoryManager repositoryManager,
            UygulamaAyarları appSet,
            List<Nobetler> OD,
            INobetlerRepository nobetlerRepository,
            Dictionary<int, int> yasakliEczaneler,
            INobetDagilimRepository nobetDagilimiRepository)
        {
            this.eczaneler = eczaneler;
            this.groupedDates = groupedDates;
            //this.nobetler = nobetler;
            _repositoryManager = repositoryManager;
            this.appSet = appSet;
            this.OD = OD;
            _nobetlerRepository = nobetlerRepository;
            this.yasakliEczaneler = yasakliEczaneler;
            _nobetDagilimiRepository = nobetDagilimiRepository;
        }
        #endregion

        #region Functions
        public async Task NobetleriDagit()
        {
            #region Definitions
            List<DateTime> weekdays = groupedDates.Weekdays;//248
            List<DateTime> saturdays = groupedDates.Saturdays;// 52
            List<DateTime> sundays = groupedDates.Sundays; //52
            List<DateTime> officialHolidays = groupedDates.OfficialHolidays; //16

            List<NobetDagilim> nobetDagilim = new List<NobetDagilim>();
            Dictionary<int, int> eczaneNobetSayilari = new Dictionary<int, int>();
            var nobetBilgisi = _nobetDagilimiRepository.GetAllNobetDagilim(1);
            foreach (var eczane in nobetBilgisi)
            {
                eczaneNobetSayilari[eczane.EczaneId] = eczane.NobetSayisi;
            }

            int eczaneCount = eczaneler.Count;
            int WDutyCount = appSet.HaftaiciNS;
            int SaDutyCount = appSet.CumartesiNS;
            int SuDutyCount = appSet.PazarNS;
            int OHDutyCount = appSet.ResmiNS;
            var p = 0;


            #endregion

            #region Resmi Tatiller
            foreach (var tatilGun in officialHolidays)
            {
                List<int> alreadyCheckedIds = new List<int>();
                for (int i = 0; i < OHDutyCount; i++)
                {
                    var OHDC = _nobetDagilimiRepository.GetMinNobetCount(1);
                    EczaneBilgileri nobetciEczane = null;
                    while (true)
                    {

                        nobetciEczane = GetNobetciEczane(tatilGun, alreadyCheckedIds);
                        if (nobetciEczane == null)
                        {
                            break;
                        }
                        else
                        {
                            if (eczaneNobetSayilari[nobetciEczane.Id] <= OHDC)
                            {
                                eczaneNobetSayilari[nobetciEczane.Id]++;
                                break;
                            }
                            else
                            {
                                p++;
                                if (eczaneCount == p)
                                {
                                    OHDC++;
                                    p = 0;
                                }
                            }
                        }
                    }
                    if (nobetciEczane != null)
                    {

                        OD.Add(new Nobetler
                        {
                            EczaneBilgileriId = nobetciEczane.Id,
                            NobetTuru = "Resmi Tatil",
                            Tarih = tatilGun.Date,
                            Acıklama = "Resmi Tatil Nöbeti"
                        });
                        var yeniNobet = OD.Last();
                        yeniNobet.EczaneBilgileri = nobetciEczane;
                        Update(nobetciEczane.Id, 4);
                    }
                }
            }
            nobetBilgisi = _nobetDagilimiRepository.GetAllNobetDagilim(2);
            foreach (var eczane in nobetBilgisi)
            {
                eczaneNobetSayilari[eczane.EczaneId] = eczane.NobetSayisi;
            }
            p = 0;
            #endregion

            #region Cumartesi
            foreach (var saturday in saturdays)
            {
                List<int> alreadyCheckedIds = new List<int>();
                for (int i = 0; i < SuDutyCount; i++)
                {
                    var SaDC = _nobetDagilimiRepository.GetMinNobetCount(2);
                    EczaneBilgileri nobetciEczane = null;
                    while (true)
                    {

                        nobetciEczane = GetNobetciEczane(saturday, alreadyCheckedIds);
                        if (nobetciEczane == null)
                        {
                            break;
                        }
                        else
                        {
                            if (eczaneNobetSayilari[nobetciEczane.Id] <= SaDC)
                            {
                                eczaneNobetSayilari[nobetciEczane.Id]++;
                                break;
                            }
                            else
                            {
                                p++;
                                if (eczaneCount == p)
                                {
                                    SaDC++;
                                    p = 0;
                                }
                            }
                        }
                    }
                    if (nobetciEczane != null)
                    {
                        OD.Add(new Nobetler
                        {
                            EczaneBilgileriId = nobetciEczane.Id,
                            NobetTuru = "Cumartesi",
                            Tarih = saturday.Date,
                            Acıklama = "Cumartesi Nöbeti"
                        });
                        var yeniNobet = OD.Last();
                        yeniNobet.EczaneBilgileri = nobetciEczane;
                        Update(nobetciEczane.Id, 4);
                    }
                }
            }
            nobetBilgisi = _nobetDagilimiRepository.GetAllNobetDagilim(3);
            foreach (var eczane in nobetBilgisi)
            {
                eczaneNobetSayilari[eczane.EczaneId] = eczane.NobetSayisi;
            }
            p = 0;
            #endregion

            #region Pazar
            foreach (var sunday in sundays)
            {
                List<int> alreadyCheckedIds = new List<int>();
                for (int i = 0; i < SaDutyCount; i++)
                {
                    var SuDC = _nobetDagilimiRepository.GetMinNobetCount(3);
                    EczaneBilgileri nobetciEczane = null;
                    while (true)
                    {

                        nobetciEczane = GetNobetciEczane(sunday, alreadyCheckedIds);
                        if (nobetciEczane == null)
                        {
                            break;
                        }
                        else
                        {
                            if (eczaneNobetSayilari[nobetciEczane.Id] <= SuDC)
                            {
                                eczaneNobetSayilari[nobetciEczane.Id]++;
                                break;
                            }
                            else
                            {
                                p++;
                                if (eczaneCount == p)
                                {
                                    SuDC++;
                                    p = 0;
                                }
                            }
                        }
                    }
                    if (nobetciEczane != null)
                    {
                        // Eczane nöbet tarihini güncelle
                        OD.Add(new Nobetler
                        {
                            EczaneBilgileriId = nobetciEczane.Id,
                            NobetTuru = "Pazar",
                            Tarih = sunday.Date,
                            Acıklama = "Pazar Nöbeti"
                        });
                        var yeniNobet = OD.Last();
                        yeniNobet.EczaneBilgileri = nobetciEczane;
                        Update(nobetciEczane.Id, 3);
                    }
                }
            }
            nobetBilgisi = _nobetDagilimiRepository.GetAllNobetDagilim(4);
            foreach (var eczane in nobetBilgisi)
            {
                eczaneNobetSayilari[eczane.EczaneId] = eczane.NobetSayisi;
            }
            p = 0;
            #endregion

            #region Haftaiçi
            foreach (var weekday in weekdays)
            {
                List<int> alreadyCheckedIds = new List<int>();
                for (int i = 0; i < WDutyCount; i++)
                {
                    var WDC = _nobetDagilimiRepository.GetMinNobetCount(4);
                    EczaneBilgileri nobetciEczane = null;
                    while (true)
                    {

                        nobetciEczane = GetNobetciEczane(weekday, alreadyCheckedIds);
                        if (nobetciEczane == null)
                        {
                            //nobetciEczane = GetNobetciEczane(weekday, alreadyCheckedIds);
                            break;
                        }
                        else
                        {
                            if (eczaneNobetSayilari[nobetciEczane.Id] <= WDC)
                            {
                                eczaneNobetSayilari[nobetciEczane.Id]++;
                                break;
                            }
                            else
                            {
                                //if (eczaneNobetSayilari.ContainsKey(nobetciEczane.Id))
                                //{
                                //    int nobetSayisi = eczaneNobetSayilari[nobetciEczane.Id];

                                //    if (nobetSayisi > WDC)
                                //    {
                                //       
                                //    }
                                //    else
                                //    {
                                //        // nobetSayisi OHDC'den küçükse
                                //    }
                                //}

                                //p++;
                                //if (eczaneCount == p)
                                //{
                                //    WDC++;
                                //    alreadyCheckedIds.Clear();
                                //    p = 0;
                                //}
                            }
                        }
                    }
                    if (nobetciEczane != null)
                    {
                        // Eczane nöbet tarihini güncelle
                        OD.Add(new Nobetler
                        {
                            EczaneBilgileriId = nobetciEczane.Id,
                            NobetTuru = "Haftaiçi", // NobetTuru'nu doldur
                            Tarih = weekday.Date,
                            Acıklama = "Haftaiçi Nöbeti"
                        });
                        var yeniNobet = OD.Last();
                        yeniNobet.EczaneBilgileri = nobetciEczane;
                        Update(nobetciEczane.Id, 4);
                    }
                }
            }
            #endregion

            await Save(OD);
        }
        private void Update(int Id, int tur)
        {
            var nobetBilgi = _nobetDagilimiRepository.GetEczaneNobetBilgisiById(Id, tur);
            _nobetDagilimiRepository.Update(Id, tur, nobetBilgi.NobetSayisi + 1);
        }
        private EczaneBilgileri GetNobetciEczane(DateTime tatilGun, List<int> alreadyCheckedIds)
        {
            // 2 gün öncesine kadar nöbet tutmamış bir eczane seçme
            var nobetciEczane = eczaneler
                .Where(e => OD.All(n => n.EczaneBilgileriId != e.Id || (tatilGun - n.Tarih).TotalDays > appSet.NobetOSDinlenme)) // 2 gün öncesine kadar nöbet tutmamış eczaneler
                .Where(e => !alreadyCheckedIds.Contains(e.Id)) // Daha önce kontrol edilen eczaneleri hariç tut
                .OrderBy(e => Guid.NewGuid()) // Rastgele sıralama
                .FirstOrDefault();

            if (nobetciEczane != null)
            {
                //bool result = _nobetlerRepository.GetLastNobetDate(a, tatilGun);
                alreadyCheckedIds.Add(nobetciEczane.Id); // Kontrol edilen eczaneleri listeye ekle

                // Diğer eczanelerle uzaklık kontrolü
                var otherEczaneler = eczaneler
                    .Where(e => e.Id != nobetciEczane.Id)
                    .Where(e => OD.Any(n => n.EczaneBilgileriId == e.Id && n.Tarih == tatilGun.Date));

                foreach (var otherEczane in otherEczaneler)
                {
                    double distance = MesafeHesaplayici.Haversine(nobetciEczane.EczaneKordinatLatitude, nobetciEczane.EczaneKordinatLongitude, otherEczane.EczaneKordinatLatitude, otherEczane.EczaneKordinatLongitude);

                    // Uzaklık kontrolü yapabilirsiniz
                    if (distance < appSet.EczaneAramaCap)
                    {
                        // Seçilen eczaneyle 1 km'den daha yakın ve nöbet sayıları dengeli olduğu için başka bir eczane seç
                        nobetciEczane = GetNobetciEczane(tatilGun, alreadyCheckedIds);
                        break;
                    }
                }
            }
            else
            {
                nobetciEczane = eczaneler
                .Where(e => OD.All(n => n.EczaneBilgileriId != e.Id || (tatilGun - n.Tarih).TotalDays > appSet.NobetOSDinlenme))
                .FirstOrDefault();
            }


            return nobetciEczane;
        }


        private async Task Save(List<Nobetler> nobetler)
        {
            try
            {
                if (_repositoryManager != null && _repositoryManager.Nobetler != null)
                {
                    await _repositoryManager.Nobetler.CreateAsyncList(nobetler);
                }
                else
                {
                    Console.WriteLine("RepositoryManager or Nobetler property is null.");
                    // Loglama kodları buraya eklenebilir
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //await Save(nobetler);
            }
        }
        #endregion

    }
}
//var WDC = _nobetDagilimiRepository.GetMinNobetCount(4);
//EczaneBilgileri nobetciEczane = null;
//while (nobetciEczane == null || eczaneNobetSayilari[nobetciEczane.Id] >= WDC)
//{
//    nobetciEczane = GetNobetciEczane(weekday, alreadyCheckedIds);
//    if (nobetciEczane == null)
//    {
//        break;
//    };
//}
//if (nobetciEczane != null)
//{
//    eczaneNobetSayilari[nobetciEczane.Id]++;
//    Eczane nöbet tarihini güncelle
//    OD.Add(new Nobetler
//    {
//        EczaneBilgileriId = nobetciEczane.Id,
//        NobetTuru = "Haftaiçi", // NobetTuru'nu doldur
//        Tarih = weekday.Date,
//        Acıklama = "Haftaiçi Nöbeti"
//    });
//    var yeniNobet = OD.Last();
//    yeniNobet.EczaneBilgileri = nobetciEczane;
//}