using CarExplorer.Models;
using CarExplorer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        public HomeController(ICarService carService)
        {
           _carService = carService;
        }
        public async Task<IActionResult> Index()
        {
            var makes = await _carService.GetMakesAsync();
            return View(makes);
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
