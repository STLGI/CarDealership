using CarDealership.Models;
using CarDealership.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarDealership.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<Car> cars;
        List<Company> companies;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            Company none = new Company(0, "None", "None", "img/cross.png");
            Company mercedes = new Company(1, "Mercedes-Benz", "Mercedes", "img/mercedes.png");
            Company toyota = new Company(2, "Toyota Motor Corporation", "Toyota", "img/toyota.png");
            Company audi = new Company(3, "Audi AG", "Audi", "img/audi.png");
            Company volkswagen = new Company(4, "BMW AG", "BMW", "img/volkswagen.png");
            companies= new List<Company> { none, mercedes, toyota, audi, volkswagen };
            cars = new List<Car>
            {
                new Car (1, "W124", mercedes, "auto", "Gasoline", 370000, 5000, 10),
                new Car (2, "Supra", toyota, "manual", "Gasoline", 0, 17000),
                new Car (3, "A8 D5", audi, "auto", "Gasoline", 0, 60000),
                new Car (4, "A8 D5", audi, "auto", "Gasoline", 0, 60000),
                new Car (5, "A8 D5", audi, "auto", "Gasoline", 0, 60000)
            };
        }

        public IActionResult Index(int? companyID) 
        {

            IndexViewModel viewModel = new IndexViewModel() { Companies = companies, Cars = cars};
            if(companyID != null && companyID != 0)
            {
                viewModel.Cars = cars.Where(c=>c.Manufacturer.Id == companyID);
            }
            return View(viewModel);
        }

        public IActionResult Vehicle(int carID)
        {
            return View(cars.Find(c => c.Id == carID));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}