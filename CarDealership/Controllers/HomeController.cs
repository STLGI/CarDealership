using CarDealership.Models;
using CarDealership.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Data;


namespace CarDealership.Controllers
{



    
    public class HomeController : Controller
    {
        private readonly CarRepository _carRepo;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHost;


        public HomeController(ILogger<HomeController> logger, CarRepository carRepo, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _carRepo = carRepo;
            _webHost = webHost;
        }

        public IActionResult Index(int? companyID = null)
        {
            IndexViewModel viewModel = new IndexViewModel() { Companies = _carRepo.Companies, Cars = _carRepo.Cars };
            if (companyID != null && companyID != 0)
            {
                viewModel.Cars = _carRepo.Cars.Where(c => c.ManufacturerId == companyID);
            }
            return View(viewModel);
        }

        public IActionResult Vehicle(int carID, int imageID = 1)
        {
            VehicleViewModel viewModel = new VehicleViewModel { VehicleCar = _carRepo.Cars.Find(c => c.Id == carID), mainImageId = imageID, Companies = _carRepo.Companies };
            return View(viewModel);
        }

        public IActionResult Sell(bool InfoException = false)
        {

            SellViewModel model = new SellViewModel();
            model.Index.Cars = _carRepo.Cars;
            model.Index.Companies = _carRepo.Companies;
            model.NotEnoughInfoException = InfoException;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(SellViewModel model)
        {

            if (model.Car.ManufacturerId == 0 || string.IsNullOrEmpty(model.Car.Model) || string.IsNullOrEmpty(model.Car.Transmission) || string.IsNullOrEmpty(model.Car.Fuel) || model.Car.MileAge == 0 || model.Car.Price == 0 || model.Files == null || model.Files.Count == 0)
            {
                return RedirectToAction("Sell", new { InfoException = true });

            }
            int i = 1;
            Car AddedCar = _carRepo.AddNewCar(model.Car with
            {
                ManufacturerId = model.Car.ManufacturerId,
                pics = model.Files.Count()
            }) ;
            foreach (var file in model.Files)
            {
                var fileName = (AddedCar.Id) + "-" + i.ToString() + '.' + file.ContentType.Split('/')[1].Trim();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                i++;
            }
            _carRepo.Cars.Add(AddedCar);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}