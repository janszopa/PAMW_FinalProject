namespace CarRental.Shared.DTO
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; } // PK
        public int RentalId { get; set; } // FK
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
    }
}