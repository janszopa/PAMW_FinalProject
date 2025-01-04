using CarRental.Shared.DTO;

namespace CarRental.Shared.Models
{
    public class Payment
    {
        public int PaymentId { get; set; } // PK
        public int RentalId { get; set; } // FK
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }

        // Nawigacja
        public Rental Rental { get; set; }
    }
}
