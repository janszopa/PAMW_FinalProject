using CarRental.Shared.DTO;

namespace CarRental.Shared.Models
{
    public class Customer
    {
        public int CustomerId { get; set; } // PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicenseNumber { get; set; }

        // Relacja
        public ICollection<Rental> Rentals { get; set; }
    }
}
