using CarRental.Shared.DTO;

namespace CarRental.Shared.Models
{
    public class CarMaintenance
    {
        public int CarMaintenanceId { get; set; } // PK
        public int CarId { get; set; } // FK
        public DateTime ServiceDate { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        // Nawigacja
        public Car Car { get; set; }
    }
}
