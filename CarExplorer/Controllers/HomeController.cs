using CarExplorer.Models;
using CarExplorer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMakes()
        {
            var makes = await _carService.GetAllMakesAsync();
            return Json(makes);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleTypes(int makeId)
        {
            var vehicleTypes = await _carService.GetVehicleTypesAsync(makeId);
            return Json(vehicleTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetModels(int makeId, int year, int page = 1, int pageSize = 10)
        {
            var allModels = await _carService.GetModelsAsync(makeId, year);
            var totalCount = allModels.Count();
            var models = allModels
                              .Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .Select(x => new
                              {
                                  x.Model_ID,
                                  x.Model_Name
                              })
                              .ToList();

            return Json(new
            {
                items = models,
                totalPages = Math.Ceiling(
                    (double)totalCount / pageSize
                )
            });
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