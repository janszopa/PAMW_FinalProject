using Microsoft.AspNetCore.Mvc;
using CarRental.Shared.Models;
using CarRental.API.Data;
using Microsoft.EntityFrameworkCore;
using CarRental.Shared.DTO;

namespace CarRental.API.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDTO>>> GetCars()
        {
            var cars = await _context.Cars
                .Select(c => new CarDTO
                {
                    CarId = c.CarId,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    RentalPrice = c.RentalPrice,
                    AvailabilityStatus = c.AvailabilityStatus
                })
                .ToListAsync();

                return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetCar(int id)
        {
            var car = await _context.Cars
                .Where(c => c.CarId == id)
                .Select(c => new CarDTO
                {
                    CarId = c.CarId,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    RentalPrice = c.RentalPrice,
                    AvailabilityStatus = c.AvailabilityStatus
                })
                .FirstOrDefaultAsync();

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CarDTO carDTO)
        {
            // Mapowanie DTO na encję
            var car = new Car
            {
                Brand = carDTO.Brand,
                Model = carDTO.Model,
                Year = carDTO.Year,
                RentalPrice = carDTO.RentalPrice,
                AvailabilityStatus = carDTO.AvailabilityStatus,
                Rentals = new List<Rental>(),
                CarMaintenances = new List<CarMaintenance>()
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            // Zwracamy stworzony zasób z odpowiednim kodem statusu
            return CreatedAtAction(nameof(GetCar), new { id = car.CarId }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarDTO carDTO)
        {
            // Znajdź istniejący samochód w bazie danych
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound(); // Zwraca 404, jeśli samochód nie istnieje
            }

            // Zaktualizuj właściwości obiektu na podstawie DTO
            car.Brand = carDTO.Brand;
            car.Model = carDTO.Model;
            car.Year = carDTO.Year;
            car.RentalPrice = carDTO.RentalPrice;
            car.AvailabilityStatus = carDTO.AvailabilityStatus;

            // Zapisz zmiany w bazie danych
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Obsługa konfliktów równoczesnych (jeśli wystąpią)
                if (!_context.Cars.Any(c => c.CarId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Zwróć status 204 No Content
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}