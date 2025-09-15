using CarDealership.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;



namespace CarDealership.Models
{

    public class CarRepository
    {


        //SqlConnection connection = new SqlConnection("server=(localdb)\\mssqllocaldb;Integrated Security=True");
        public List<Car>? Cars { get; set; }
        public List<Company> Companies { get; set; }

        //public string? carsJson { get; set; }


        public CarRepository()
        {

            //var tablesExist = connection.QueryFirstOrDefault<int>(
            //       "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN ('Cars', 'Companies')"); // check for the tables "Cars“ and "Companies"
            //
            //if (tablesExist < 2) { connection.Execute(ReadSqlScript()); } // if at least one of them does not exist create both

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            using (var context = new DealershipDbContext(configuration))
            {
                Companies = context.Companies.ToList();
                Cars = context.Cars.ToList();
                context.SaveChanges();
            }


        }
        public Car AddNewCar(Car car)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            Car car1;
            using (var context = new DealershipDbContext(configuration))
            {
                context.Cars.Add(car);
                context.SaveChanges();
                car1 = context.Cars.OrderBy(c => c.Id).LastOrDefault() ;


            }
            return car1;
        }
        /*private string ReadSqlScript()
        { 
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Models", "TableCreator.sql");
            return File.ReadAllText(filePath);
            }*/
    }
}
