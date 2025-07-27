using Dapper;
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

        SqlConnection connection = new SqlConnection("server=(localdb)\\mssqllocaldb;Integrated Security=True");
        public List<Car>? Cars { get; set; }
        public List<Company> Companies { get; set; }

        public string? carsJson { get; set; }

        public CarRepository()
        {
            var tablesExist = connection.QueryFirstOrDefault<int>(
                   "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN ('Cars', 'Companies')"); // check for the tables "Cars“ and "Companies"

            if (tablesExist < 2) { connection.Execute(ReadSqlScript()); } // if at least one of them does not exist create both

            Companies = connection.Query<Company>("SELECT * FROM Companies").ToList();
            Cars = connection.Query<Car>("SELECT * FROM Cars").ToList();
        }
        public void AddNewCar()
        {
            connection.Query("INSERT INTO Cars (Id, Model, ManufacturerId, Transmission, Fuel, MileAge, Price, pics) VALUES (@Id, @Model, @ManufacturerId, @Transmission, @Fuel, @MileAge, @Price, @pics);", Cars[Cars.Count - 1]);
        }
        private string ReadSqlScript()
        { 
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Models", "TableCreator.sql");
            return File.ReadAllText(filePath);
        }
    }
}
