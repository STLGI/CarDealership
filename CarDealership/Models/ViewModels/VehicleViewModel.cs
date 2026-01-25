namespace CarDealership.Models.ViewModels
{
    public class VehicleViewModel
    {
        public Car? VehicleCar { get; set; }
        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
    }
}
