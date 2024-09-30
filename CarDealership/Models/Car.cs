namespace CarDealership.Models
{
    public record class Car(int Id, string Model, Company Manufacturer, string Transmission, string Fuel, int MileAge, int Price) { };

}
