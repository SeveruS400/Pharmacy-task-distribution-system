using EzcaneBilgiSistemi.Models;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EzcaneBilgiSistemi.Controllers
{
    public class EczanelerController : Controller
    {
        #region Ctor
        private readonly IRepositoryManager _manager;
        private readonly ISehirRepository _sehirRepository;
        private readonly IBolgelerRepository _bolgelerRepository;
        public EczanelerController(IRepositoryManager manager, ISehirRepository sehirRepository, IBolgelerRepository bolgelerRepository)
        {
            _manager = manager;
            _sehirRepository = sehirRepository;
            _bolgelerRepository = bolgelerRepository;
        }
        #endregion
        public IActionResult Index(int? page)
        {
            if (page == null || page == 0)
            {
                #region ViewBag
                var bolgelist = _bolgelerRepository.GetAll(false).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.BolgeName
                }).ToList();
                ViewBag.Bolgeler = bolgelist;

                var sehirList = _sehirRepository.GetAll(false).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Sehir
                }).ToList();
                ViewBag.Sehirler = sehirList;
                #endregion

                //ViewModel Model = new ViewModel()
                //{
                //    pageCount = 1,
                //    EczaneBilgileri = new EczaneBilgileri() { }
                //};
                //return View(Model);
                EczaneBilgileri Model = new EczaneBilgileri();
                ViewBag.pageCount = 1;
                return View(Model);
            }
            else
            {
                ViewBag.pageCount = page;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Table(string Text, int page, string sortBy)
        {
            var pageNumber = page;
            if (pageNumber == null || pageNumber == 0)
            {
                pageNumber = 1;
            }

            int pageSize = 10;
            var searchText = Text;

            // searchText boş değilse filtrele
            if (!string.IsNullOrEmpty(searchText))
            {
                ViewData["RenderPartial"] = true;
                var eczaneBilgileriList = _manager.EzcaneBilgileri.GetAll(false);
                eczaneBilgileriList = eczaneBilgileriList
                    .Where(a => a.EczaneAdi.ToLower().Contains(searchText.ToLower()) || a.EczaneSahibiAdi.ToLower().Contains(searchText.ToLower()));

                // Sıralama işlemi
                if (!string.IsNullOrEmpty(sortBy))
                {
                    eczaneBilgileriList = eczaneBilgileriList.OrderBy(a => a.EczaneAdi);
                }

                var eczaneBilgileri = eczaneBilgileriList
                    .Skip((pageNumber -1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                //var totalCount = eczaneBilgileri.Count();
                var totalCount = eczaneBilgileriList.Count();
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.PageCount = pageCount;

                return PartialView("_table", eczaneBilgileri);
            }
            else
            {
                var eczaneBilgileriList = _manager.EzcaneBilgileri.GetAll(false).ToList();

                var eczaneBilgileri = eczaneBilgileriList
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Sıralama işlemi
                if (!string.IsNullOrEmpty(sortBy))
                {
                    if (sortBy == "EczaneAdi")
                    {
                        eczaneBilgileri = eczaneBilgileri.OrderBy(a => a.EczaneAdi).ToList();
                    }
                    //else if (sortBy == "EczaneAdresi")
                    //{
                    //    eczaneBilgileri = eczaneBilgileri.OrderBy(a => a.EczaneAdresi).ToList();
                    //}
                    //else if (sortBy == "ID")
                    //{
                    //    eczaneBilgileri = eczaneBilgileri.OrderBy(a => a.Id).ToList();
                    //}
                }
                var totalCount = eczaneBilgileriList.Count();
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
                pageNumber = page;
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageCount = pageCount;

                return PartialView("_table", eczaneBilgileri);
            }
        }

        private List<EczaneBilgileri> ApplySorting(List<EczaneBilgileri> list, string sortBy, string sortOrder)
        {
            if (sortBy == "EczaneAdi")
            {
                list = (sortOrder == "asc") ? list.OrderBy(a => a.EczaneAdi).ToList() : list.OrderByDescending(a => a.EczaneAdi).ToList();
            }
            else if (sortBy == "EczaneAdresi")
            {
                list = (sortOrder == "asc") ? list.OrderBy(a => a.EczaneAdresi).ToList() : list.OrderByDescending(a => a.EczaneAdresi).ToList();
            }
            else if (sortBy == "ID")
            {
                list = (sortOrder == "asc") ? list.OrderBy(a => a.Id).ToList() : list.OrderByDescending(a => a.Id).ToList();
            }

            return list;
        }

        [HttpPost]
        public async Task<IActionResult> EczaneKayit(EczaneBilgileri eczaneBilgileri)
        {

            await _manager.EzcaneBilgileri.CreateAsync(eczaneBilgileri);
            return RedirectToAction("Index");
        }

        [HttpPost("Duzenle")]
        public async Task<IActionResult> EczaneDuzenle(int Id, EczaneBilgileri eczaneBilgileri)
        {
            await _manager.EzcaneBilgileri.UpdateAsync(Id, eczaneBilgileri);
            return RedirectToAction("Index");
        }

        [HttpPost("Sil")]
        public async Task<IActionResult> EzcaneSil(int Id)
        {
            await _manager.EzcaneBilgileri.DeleteAsync(Id);
            return RedirectToAction("Index");
        }
    }
}
