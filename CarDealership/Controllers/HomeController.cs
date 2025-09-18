using CarDealership.Models;
using CarDealership.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CarDealership.Controllers
{




    public class HomeController : Controller
    {
        private readonly CarRepository _carRepo;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHost;

        private static bool ModelNotComplete(SellViewModel model)
        {
            return (model.Car.Manufacturer.Id == 0 || string.IsNullOrEmpty(model.Car.Model) ||
                    string.IsNullOrEmpty(model.Car.Transmission) || string.IsNullOrEmpty(model.Car.Fuel) ||
                    model.Car.MileAge == 0 || model.Car.Price == 0 || model.Files.Count == 0);
        }
        public HomeController(ILogger<HomeController> logger, CarRepository carRepo, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _carRepo = carRepo;
            _webHost = webHost;
        }

        public IActionResult Index(int? companyId = null)
        {
            var viewModel = new IndexViewModel() { Companies = _carRepo.Companies, Cars = _carRepo.Cars };
            if (companyId != null && companyId != 0 && _carRepo.Cars != null)
            {
                viewModel.Cars = _carRepo.Cars.Where(c => c.Manufacturer.Id == companyId);
            }
            return View(viewModel);
        }

        public IActionResult Vehicle(int carId, int imageId = 1)
        {
            var viewModel = new VehicleViewModel { VehicleCar = _carRepo.Cars?.Find(c => c.Id == carId), MainImageId = imageId, Companies = _carRepo.Companies };
            return View(viewModel);
        }

        public IActionResult Sell(bool infoException = false)
        {

            var model = new SellViewModel();
            model.Index.Cars = _carRepo.Cars;
            model.Index.Companies = _carRepo.Companies;
            model.NotEnoughInfoException = infoException;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(SellViewModel model)
        {

            if (ModelNotComplete(model))
            {
                return RedirectToAction("Sell", new { InfoException = true });

            }
            var i = 1;
            var addedCar = _carRepo.AddNewCar(model.Car with
            {
                Manufacturer = model.Car.Manufacturer,
                Pics = model.Files.Count()
            });
            foreach (var file in model.Files)
            {
                var fileName = (addedCar.Id) + "-" + i.ToString() + '.' + file.ContentType.Split('/')[1].Trim();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                i++;
            }
            _carRepo.Cars?.Add(addedCar);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}