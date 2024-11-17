namespace CarDealership.Models.ViewModels
{
    public class VehicleViewModel
    {
        public Car? VehicleCar { get; set; }
        public int mainImageId { get; set; }
        public string GetImageName(int carId, int picNum)
        {
            var searchPattern = "*" + carId.ToString() +"-" + picNum.ToString() + ".*";
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/wwwroot/img", searchPattern);
            return files.Length > 0 ? Path.GetFileName(files[0]) : "add-img.jpg";
        }
    }
}
