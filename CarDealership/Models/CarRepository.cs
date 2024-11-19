using Newtonsoft.Json;

namespace CarDealership.Models
{
    public class CarRepository
    {
        public List<Car>? Cars { get; set; }
        public List<Company> Companies { get; set; }

        public string? carsJson { get; set; }

        public CarRepository() {
            if(!File.Exists(Directory.GetCurrentDirectory()  + "/wwwroot/saved/carsInfo.json"))
            {
                using (FileStream fs = File.Create(Directory.GetCurrentDirectory() + "/wwwroot/saved/carsInfo.json"))
                {
                }
            }
            else
            {
                
                carsJson = File.ReadAllText(Directory.GetCurrentDirectory()  + "/wwwroot/saved/carsInfo.json");
            }
            Cars = JsonConvert.DeserializeObject<List<Car>>(carsJson);
            Company none = new Company(0, "None", "None", "img/cross.png");
            Company mercedes = new Company(1, "Mercedes-Benz", "Mercedes", "img/mercedes.png");
            Company toyota = new Company(2, "Toyota Motor Corporation", "Toyota", "img/toyota.png");
            Company audi = new Company(3, "Audi AG", "Audi", "img/audi.png");
            Company volkswagen = new Company(4, "BMW AG", "BMW", "img/volkswagen.png");
            Companies = new List<Company> { none, mercedes, toyota, audi, volkswagen };

            if (Cars == null)
            {
                Cars = new List<Car>
                {
                    new Car (1, "W124", mercedes, "auto", "Gasoline", 370000, 5000, 10),
                    new Car (2, "Supra", toyota, "manual", "Gasoline", 0, 17000),
                    new Car (3, "A8 D5", audi, "auto", "Gasoline", 0, 60000),
                    new Car (4, "A8 D5", audi, "auto", "Gasoline", 0, 60000),
                    new Car (5, "A8 D5", audi, "auto", "Gasoline", 0, 60000)
                };
            }
        }
        public void AddNewCar()
        {
            File.WriteAllText(Directory.GetCurrentDirectory() + "/wwwroot/saved/carsInfo.json", JsonConvert.SerializeObject(Cars, Formatting.Indented));
        }
    }
}
