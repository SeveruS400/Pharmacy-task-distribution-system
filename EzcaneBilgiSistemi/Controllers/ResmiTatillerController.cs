using Entities.Models;
using EzcaneBilgiSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories.Contracts;

namespace EzcaneBilgiSistemi.Controllers
{
    public class ResmiTatillerController : Controller
    {
        private readonly IRepositoryManager _manager;
        private readonly IResmiTatillerRepository _resmiTatillerRepository;
        public ResmiTatillerController(IRepositoryManager manager, IResmiTatillerRepository resmiTatillerRepository)
        {
            _manager = manager;
            _resmiTatillerRepository = resmiTatillerRepository;
        }
        public IActionResult Index(int? page)
        {
            if (page==0)
            {
                ViewBag.page = 0;
            }
            else
            {
                ViewBag.page = page;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResmiTatilKayit(ResmiTatiller resmiTatiller)
        {

            await _manager.ResmiTatiller.CreateAsync(resmiTatiller);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Table(string Text, int page)
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
                var resmiTatillerList = _manager.ResmiTatiller.GetAll(false);
                resmiTatillerList = resmiTatillerList
                    .Where(a => a.Acıklama.ToLower().Contains(searchText.ToLower()));

                // Sıralama işlemi

                resmiTatillerList = resmiTatillerList.OrderBy(a => a.Tarih);


                var resmiTatiller = resmiTatillerList
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                //var totalCount = eczaneBilgileri.Count();
                var totalCount = resmiTatillerList.Count();
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.PageCount = pageCount;

                return PartialView("_resmiTatilTablo", resmiTatillerList);
            }
            else
            {
                var resmiTatillerList = _manager.ResmiTatiller.GetAll(false).ToList();

                var resmiTatiller = resmiTatillerList
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                //// Sıralama işlemi
                //if (!string.IsNullOrEmpty(sortBy))
                //{
                //    if (sortBy == "EczaneAdi")
                //    {
                //        eczaneBilgileri = eczaneBilgileri.OrderBy(a => a.EczaneAdi).ToList();
                //    }

                //}
                var totalCount = resmiTatillerList.Count();
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
                pageNumber = page;
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageCount = pageCount;

                return PartialView("_resmiTatilTablo", resmiTatiller);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResmiTatilDuzenle(int Id, ResmiTatiller resmiTatiller)
        {
            await _manager.ResmiTatiller.UpdateAsync(Id, resmiTatiller);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ResmiTatilSil(int Id)
        {
            await _manager.ResmiTatiller.DeleteAsync(Id);
            return RedirectToAction("Index");
        }
    }


}
