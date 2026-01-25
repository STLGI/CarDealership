namespace CarDealership.Models.ViewModels
{
    public class SellViewModel
    {
        public List<IFormFile> Files { get; set; } = null!;
        public IndexViewModel Index { get; set; } = new IndexViewModel();

        public Car? Car { get; set; }

        public bool NotEnoughInfoException { get; set; } = false;

    }
}
