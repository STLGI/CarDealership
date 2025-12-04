namespace CarDealership.Models
{
    public record Company
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string SName { get; set; }
        public required string Img { get; set; }
    }
}
