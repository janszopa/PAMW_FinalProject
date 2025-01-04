using Microsoft.AspNetCore.Mvc;
using CarRental.Shared.Models;
using CarRental.API.Data;
using Microsoft.EntityFrameworkCore;
using CarRental.Shared.DTO;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetRentals()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Select(r => new RentalDTO
                {
                    RentalId = r.RentalId,
                    CarId = r.CarId,
                    CarBrand = r.Car.Brand,
                    CarModel = r.Car.Model,
                    CustomerId = r.CustomerId,
                    CustomerFirstName = r.Customer.FirstName,
                    CustomerLastName = r.Customer.LastName,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    TotalPrice = r.TotalPrice
                })
                .ToListAsync();

            return Ok(rentals);
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDTO>> GetRental(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Where(r => r.RentalId == id)
                .Select(r => new RentalDTO
                {
                    RentalId = r.RentalId,
                    CarId = r.CarId,
                    CarBrand = r.Car.Brand,
                    CarModel = r.Car.Model,
                    CustomerId = r.CustomerId,
                    CustomerFirstName = r.Customer.FirstName,
                    CustomerLastName = r.Customer.LastName,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    TotalPrice = r.TotalPrice
                })
                .FirstOrDefaultAsync();

            if (rental == null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        // PUT: api/Rentals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, PostRentalDTO rentalDTO)
        {
            if (id != rentalDTO.RentalId)
            {
                return BadRequest();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            rental.CarId = rentalDTO.CarId;
            rental.CustomerId = rentalDTO.CustomerId;
            rental.StartDate = rentalDTO.StartDate;
            rental.EndDate = rentalDTO.EndDate;
            rental.TotalPrice = rentalDTO.TotalPrice;

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rentals
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental(PostRentalDTO rentalDTO)
        {
            var car = await _context.Cars.FindAsync(rentalDTO.CarId);
            var customer = await _context.Customers.FindAsync(rentalDTO.CustomerId);

            if (car == null || customer == null)
            {
                return BadRequest(new { error = "Invalid CarId or CustomerId. Car or Customer does not exist." });
            }

            var rental = new Rental
            {
                CarId = rentalDTO.CarId,
                CustomerId = rentalDTO.CustomerId,
                StartDate = rentalDTO.StartDate,
                EndDate = rentalDTO.EndDate,
                TotalPrice = rentalDTO.TotalPrice,
                Car = car,
                Customer = customer
            };

            if (car.Rentals == null)
            {
                car.Rentals = new List<Rental>();
            }

            if (customer.Rentals == null)
            {
                customer.Rentals = new List<Rental>();
            }

            car.Rentals.Add(rental);
            customer.Rentals.Add(rental);

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            rentalDTO.RentalId = rental.RentalId;

            return CreatedAtAction("GetRental", new { id = rental.RentalId }, rentalDTO);
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalId == id);
        }
    }
}