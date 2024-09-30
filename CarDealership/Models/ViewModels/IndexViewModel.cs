namespace CarDealership.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
        public IEnumerable<Company> Companies { get; set; } = new List<Company>(); 
    }
}
