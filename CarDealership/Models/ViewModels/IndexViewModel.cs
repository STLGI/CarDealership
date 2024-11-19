namespace CarDealership.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
        public string GetImageName(int carId)
        {
            var searchPattern = "*" + carId.ToString() + "-1.*";
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/wwwroot/img", searchPattern);
            return files.Length > 0 ? Path.GetFileName(files[0]) : "add-img.jpg";
        }
    }
}
