namespace CarRental.Shared.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; } // PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicenseNumber { get; set; }
    }
}