using CarRental.Shared.DTO;

namespace CarRental.Shared.Models
{
    public class Car
    {
        public int CarId { get; set; } // PK
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal RentalPrice { get; set; }
        public bool AvailabilityStatus { get; set; }

        public ICollection<Rental> Rentals { get; set; }
        public ICollection<CarMaintenance> CarMaintenances { get; set; }
    }
}
