using CarDealership.web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Sell()
        {
            return View();
        }
        public IActionResult Vehicle(int carId, int? mainImageID = 1)
        {
            var model = new WebVehicleViewModel { CarId = carId, MainImageId = mainImageID };
            Console.WriteLine(carId);
            return View(model);
        }
    }
}
