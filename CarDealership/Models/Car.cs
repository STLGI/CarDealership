namespace CarDealership.Models
{
    public record class Car(int Id, string Model, int ManufacturerId, string Transmission, string Fuel, int MileAge, int Price, int? pics = 0) { };

}
