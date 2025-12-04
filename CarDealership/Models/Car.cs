namespace CarDealership.Models
{
    public record Car
    {
        public int Id { get; set; }
        public required string Model { get; set; }
        public int? ManufacturerId { get; set; } = null;
        public Company? Manufacturer { get; set; } = null;
        public required string Transmission { get; set; }
        public required string Fuel { get; set; }
        public int MileAge { get; set; }
        public int Price { get; set; }
        public ICollection<CarImage>? Images { get; set; } = new List<CarImage>();
    }
    public record CarImage
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required byte[] Data { get; set; }
        public int CarId { get; set; }
        public required Car Car { get; set; }

    }

}