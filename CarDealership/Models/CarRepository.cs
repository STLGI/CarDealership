namespace CarDealership.Models
{

    public class CarRepository
    {
        private readonly DealershipDbContext _context;


        //SqlConnection connection = new SqlConnection("server=(localdb)\\mssqllocaldb;Integrated Security=True");
        public List<Car>? Cars { get; set; }
        public List<Company> Companies { get; set; }

        //public string? carsJson { get; set; }


        public CarRepository(DealershipDbContext context)
        {

            //var tablesExist = connection.QueryFirstOrDefault<int>(
            //       "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN ('Cars', 'Companies')"); // check for the tables "Cars“ and "Companies"
            //
            //if (tablesExist < 2) { connection.Execute(ReadSqlScript()); } // if at least one of them does not exist create both

            _context = context;
            Companies = _context.Companies.ToList();
            Cars = _context.Cars.ToList();
        }

        public Car? AddNewCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return _context.Cars.OrderBy(c => c.Id).LastOrDefault();
        }
        /*private string ReadSqlScript()
        { 
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Models", "TableCreator.sql");
            return File.ReadAllText(filePath);
            }*/
    }
}
