using CarDealership.Models;
using CarDealership.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CarDealership.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
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
        [HttpGet("Index/{companyId?}")]

        public IActionResult Index(int? companyId = null)
        {
            var viewModel = new IndexViewModel() { Companies = _carRepo.Companies, Cars = _carRepo.Cars };
            if (companyId != null && companyId != 1 && _carRepo.Cars != null)
            {
                viewModel.Cars = _carRepo.Cars.Where(c => c.Manufacturer.Id == companyId);
            }
            return Ok(viewModel);
        }
        [HttpGet("{carId}/images/{imageId}")]
        public IActionResult GetCarImage(int carId, int imageId)
        {
            Console.WriteLine("GetImage HIT: carId=" + carId + " imageId=" + imageId);
            var image = _carRepo.Cars
                .FirstOrDefault(c => c.Id == carId)?
                .Images?.FirstOrDefault(i => i.Id == imageId);
            Console.WriteLine(_carRepo.Cars.FirstOrDefault(c => c.Id == carId)?.Images?.Count);
            if (image == null)
            {
                return NotFound();
            }
            Console.WriteLine(image.ContentType);
            return File(image.Data, image.ContentType);
        }

        [HttpGet("vehicle/{carId}")]
        public IActionResult Vehicle(int carId)
        {
            var viewModel = new VehicleViewModel { VehicleCar = _carRepo.Cars?.Find(c => c.Id == carId), Companies = _carRepo.Companies };
            return Ok(viewModel);
        }
        [HttpGet("sell")]
        public IActionResult Sell(bool infoException = false)
        {

            var model = new SellViewModel();
            model.Index.Cars = _carRepo.Cars;
            model.Index.Companies = _carRepo.Companies;
            model.NotEnoughInfoException = infoException;
            return Ok(model);
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCar([FromForm] SellViewModel model)
        {
            Console.WriteLine(model.Car);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Car != null)
            {
                model.Car.Manufacturer = _carRepo.Companies.Where(c => c.Id == model.Car.ManufacturerId).FirstOrDefault();
            }
            if (ModelNotComplete(model))
            {
                return BadRequest(new { message = "All fields have to be filled and at least one image is required!" });

            }
            _carRepo.AddNewCar(model.Car with
            {
                Manufacturer = model.Car.Manufacturer
            }, model.Files);

            return Ok(new { message = "Car added successfully" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}