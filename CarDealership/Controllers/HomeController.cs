using CarDealership.Models;
using CarDealership.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;

namespace CarDealership.Controllers
{




    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private static List<Car> cars;
        private List<Company> companies;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHost)
        {
            _logger = logger;
            Company none = new Company(0, "None", "None", "img/cross.png");
            Company mercedes = new Company(1, "Mercedes-Benz", "Mercedes", "img/mercedes.png");
            Company toyota = new Company(2, "Toyota Motor Corporation", "Toyota", "img/toyota.png");
            Company audi = new Company(3, "Audi AG", "Audi", "img/audi.png");
            Company volkswagen = new Company(4, "BMW AG", "BMW", "img/volkswagen.png");
            companies = new List<Company> { none, mercedes, toyota, audi, volkswagen };
            if (cars == null)
            {
                cars = new List<Car>
                {
                    new Car (1, "W124", mercedes, "auto", "Gasoline", 370000, 5000, 10),
                    new Car (2, "Supra", toyota, "manual", "Gasoline", 0, 17000),
                    new Car (3, "A8 D5", audi, "auto", "Gasoline", 0, 60000),
                    new Car (4, "A8 D5", audi, "auto", "Gasoline", 0, 60000),
                    new Car (5, "A8 D5", audi, "auto", "Gasoline", 0, 60000)
                };
            }
            _webHost = webHost;
        }

        public IActionResult Index(int? companyID = null)
        {

            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + "/wwwroot/uploads"))
            {
                System.IO.File.Delete(file);
            }
            IndexViewModel viewModel = new IndexViewModel() { Companies = companies, Cars = cars };
            if (companyID != null && companyID != 0)
            {
                viewModel.Cars = cars.Where(c => c.Manufacturer.Id == companyID);
            }
            return View(viewModel);
        }

        public IActionResult Vehicle(int carID, int imageID = 1)
        {
            VehicleViewModel viewModel = new VehicleViewModel { VehicleCar = cars.Find(c => c.Id == carID), mainImageId = imageID };
            return View(viewModel);
        }

        public IActionResult Sell(bool InfoException = false)
        {

            SellViewModel model = new SellViewModel();
            model.Index.Cars = cars;
            model.Index.Companies = companies;
            model.NotEnoughInfoException = InfoException;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(SellViewModel model)
        {
            string substring = "*" + (cars.Count + 1) + "-" + "*";

            if (model.Car.Manufacturer.Id == 0 || string.IsNullOrEmpty(model.Car.Model) || string.IsNullOrEmpty(model.Car.Transmission) || string.IsNullOrEmpty(model.Car.Fuel) || model.Car.MileAge == 0 || model.Car.Price == 0 || model.Files == null || model.Files.Count == 0)
            {
                return RedirectToAction("Sell", new { InfoException = true });

            }
            int i = 0;
            foreach (var file in model.Files)
            {
                var fileName = (cars.Count + 1) + "-" + i.ToString() + '.' + file.ContentType.Split('/')[1].Trim();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                i++;
            }
            cars.Add(new Car(cars.Count + 1, model.Car.Model, companies[model.Car.Manufacturer.Id], model.Car.Transmission, model.Car.Fuel, model.Car.MileAge, model.Car.Price, model.Files.Count()));
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}