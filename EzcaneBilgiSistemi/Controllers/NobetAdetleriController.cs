using EzcaneBilgiSistemi.Models;
using EzcaneBilgiSistemi.Services;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace EzcaneBilgiSistemi.Controllers
{
    public class NobetAdetleriController : Controller
    {
        private readonly NobetRaporServisi _nobetRaporServisi;

        public NobetAdetleriController(NobetRaporServisi nobetRaporServisi)
        {
            _nobetRaporServisi = nobetRaporServisi;
        }
        public IActionResult Index()
        {
            var nobetRapor = _nobetRaporServisi.GetNobetRapor();
            return View(nobetRapor);
        }
    }
}
