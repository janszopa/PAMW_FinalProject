namespace CarRental.Shared.DTO
{
    public class RentalDTO
    {
        public int RentalId { get; set; } // PK
        public int CarId { get; set; } // FK
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public int CustomerId { get; set; } // FK
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}