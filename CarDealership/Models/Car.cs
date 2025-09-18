namespace CarDealership.Models
{
    public record Car
    {
        public int Id { get; set; }
        public required string Model { get; set; }
        public required Company Manufacturer { get; set; }
        public required string Transmission { get; set; }
        public required string Fuel { get; set; }
        public int MileAge { get; set; }
        public int Price { get; set; }
        public int? Pics { get; set; } = 0;
    }

}