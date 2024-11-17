namespace CarDealership.Models.ViewModels
{
    public class SellViewModel
    {
        public List<IFormFile> Files { get; set; }
        public List<string> FilePaths { get; set; } = new List<string>();
        public IndexViewModel Index { get; set; } = new IndexViewModel();

        public Car Car { get; set; }
        public string FormToken { get; set; }

        public bool NotEnoughInfoException { get; set; } = false;

        public int imgNum { get; set; } = 0;
        public string GetImageName(int carId, int picNum)
        {
            var searchPattern = "*" + carId.ToString() + "-" + picNum.ToString() + ".*";
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/wwwroot/uploads", searchPattern);
            return files.Length > 0 ? Path.GetFileName(files[0]) : "add-img.jpg";
        }
    }
}
