namespace CarDealership.Models
{
    public record class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int ManufacturerId { get; set; }
        public string Transmission { get; set; }
        public string Fuel { get; set; }
        public int MileAge { get; set; }
        public int Price { get; set; }
        public int? pics { get; set; } = 0;
    }

}