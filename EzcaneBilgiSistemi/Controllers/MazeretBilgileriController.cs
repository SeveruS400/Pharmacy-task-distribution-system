using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Contracts;
using Repositories.Implementations;

namespace EzcaneBilgiSistemi.Controllers
{
    public class MazeretBilgileriController : Controller
    {
        private readonly IRepositoryManager _manager;
        private readonly IEczaneBilgileriRepository _eczaneler;
        private readonly IMazeretBilgileriRepository _mazeretBilgileriRepository;
        public MazeretBilgileriController(IRepositoryManager manager, IEczaneBilgileriRepository eczaneler, IMazeretBilgileriRepository mazeretBilgileriRepository)
        {
            _manager = manager;
            _eczaneler = eczaneler;
            _mazeretBilgileriRepository = mazeretBilgileriRepository;
        }
        public IActionResult Index(int? page)
        {
            if (page == null)
            {
                ViewBag.page = 0;
            }
            else
            {
                ViewBag.page = page;
            }
            var eczaneList = _eczaneler.GetAll(false).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.EczaneAdi
            }).ToList();
            ViewBag.Eczaneler = eczaneList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MazeretBilgileriKayit(MazeretBilgileri mazeretBilgileri)
        { 
            await _manager.MazeretBilgileri.CreateAsync(mazeretBilgileri);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Table(string Text, int page)
        {
            var pageNumber = page;
            if (pageNumber == null || pageNumber == 0)
            {
                pageNumber = 1;
            }

            int pageSize = 3;
            var searchText = Text;

            var eczaneList = _eczaneler.GetAll(false).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.EczaneAdi
            }).ToList();
            ViewBag.Eczaneler = eczaneList;

            // searchText boş değilse filtrele
            if (!string.IsNullOrEmpty(searchText))
            {
                ViewData["RenderPartial"] = true;
                var mazeretBilgileriList = _manager.MazeretBilgileri.GetAll(false);
                mazeretBilgileriList = mazeretBilgileriList
                    .Where(a => a.Acıklama.ToLower().Contains(searchText.ToLower()));


                var mazeretBilgileri = mazeretBilgileriList
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                //var totalCount = eczaneBilgileri.Count();
                var totalCount = mazeretBilgileriList.Count();
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.PageCount = pageCount;

                return PartialView("_mazeretBilgileriTablo", mazeretBilgileriList);
            }
            else
            {
                var mazeretBilgileriList =await _mazeretBilgileriRepository.GetMazeretBilgi();

                var resmiTatiller = mazeretBilgileriList
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
                var totalCount = mazeretBilgileriList.Count();
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
                pageNumber = page;
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageCount = pageCount;

                return PartialView("_mazeretBilgileriTablo", mazeretBilgileriList);
            }
        }
    }
}
