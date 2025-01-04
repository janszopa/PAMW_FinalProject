namespace CarRental.Shared.DTO
{
    public class CarMaintenanceDTO 
    {
        public int CarMaintenanceId { get; set; } // PK
        public int CarId { get; set; } // FK
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public DateTime ServiceDate { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}