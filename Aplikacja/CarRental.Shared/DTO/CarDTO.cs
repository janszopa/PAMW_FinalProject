namespace CarRental.Shared.DTO
{
    public class CarDTO
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal RentalPrice { get; set; }
        public bool AvailabilityStatus { get; set; }
    }
}