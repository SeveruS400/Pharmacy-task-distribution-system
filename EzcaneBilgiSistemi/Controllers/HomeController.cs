using Entities.Models;
using EzcaneBilgiSistemi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using System.Diagnostics;

namespace EzcaneBilgiSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryManager _manager;
        private readonly INobetlerRepository _nobetlerRepository;

        public HomeController(ILogger<HomeController> logger, 
            IRepositoryManager manager,
            INobetlerRepository nobetlerRepository)
        {
            _logger = logger;
            _manager = manager;
            _nobetlerRepository = nobetlerRepository;
        }

        public async Task<IActionResult> Index()
        {
            //var userRole = HttpContext.Session.GetString("UserRole");
            var nobetciler = _nobetlerRepository.GetAllNobetlerWithin7Days();

            return View("Index", nobetciler);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}