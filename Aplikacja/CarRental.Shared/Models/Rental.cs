using CarRental.Shared.DTO;

namespace CarRental.Shared.Models
{
    public class Rental
    {
        public int RentalId { get; set; } // PK
        public int CarId { get; set; } // FK
        public int CustomerId { get; set; } // FK
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        // Nawigacja
        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}
