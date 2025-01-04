namespace CarRental.Shared.DTO
{
    public class PostCarMaintenanceDTO 
    {
        public int CarMaintenanceId { get; set; } // PK
        public int CarId { get; set; } // FK
        public DateTime ServiceDate { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}