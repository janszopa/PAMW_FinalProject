namespace CarRental.Shared.DTO
{
    public class PostRentalDTO
    {
        public int RentalId { get; set; } // PK
        public int CarId { get; set; } // FK
        public int CustomerId { get; set; } // FK
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}