using Microsoft.AspNetCore.Mvc;
using CarRental.Shared.Models;
using CarRental.API.Data;
using Microsoft.EntityFrameworkCore;
using CarRental.Shared.DTO;

namespace CarRental.API.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarMaintenanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarMaintenanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CarMaintenance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarMaintenanceDTO>>> GetCarMaintenances()
        {
            var maintenances = await _context.CarMaintenances
                .Include(cm => cm.Car)
                .Select(cm => new CarMaintenanceDTO
                {
                    CarMaintenanceId = cm.CarMaintenanceId,
                    CarId = cm.CarId,
                    CarBrand = cm.Car.Brand,
                    CarModel = cm.Car.Model,
                    ServiceDate = cm.ServiceDate,
                    Description = cm.Description,
                    Cost = cm.Cost
                })
                .ToListAsync();

            return Ok(maintenances);
        }

        // GET: api/CarMaintenance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarMaintenanceDTO>> GetCarMaintenance(int id)
        {
            var maintenance = await _context.CarMaintenances
                .Include(cm => cm.Car)
                .Where(cm => cm.CarMaintenanceId == id)
                .Select(cm => new CarMaintenanceDTO
                {
                    CarMaintenanceId = cm.CarMaintenanceId,
                    CarId = cm.CarId,
                    CarBrand = cm.Car.Brand,
                    CarModel = cm.Car.Model,
                    ServiceDate = cm.ServiceDate,
                    Description = cm.Description,
                    Cost = cm.Cost
                })
                .FirstOrDefaultAsync();

            if (maintenance == null)
            {
                return NotFound();
            }

            return Ok(maintenance);
        }

        // PUT: api/CarMaintenance/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarMaintenance(int id, PostCarMaintenanceDTO carMaintenanceDTO)
        {
            if (id != carMaintenanceDTO.CarMaintenanceId)
            {
                return BadRequest();
            }

            var carMaintenance = await _context.CarMaintenances.FindAsync(id);
            if (carMaintenance == null)
            {
                return NotFound();
            }

            carMaintenance.CarId = carMaintenanceDTO.CarId;
            carMaintenance.ServiceDate = carMaintenanceDTO.ServiceDate;
            carMaintenance.Description = carMaintenanceDTO.Description;
            carMaintenance.Cost = carMaintenanceDTO.Cost;

            _context.Entry(carMaintenance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarMaintenanceExists(id))
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

        // POST: api/CarMaintenance
        [HttpPost]
        public async Task<ActionResult<CarMaintenance>> PostCarMaintenance(PostCarMaintenanceDTO carMaintenanceDTO)
        {
            var car = await _context.Cars.FindAsync(carMaintenanceDTO.CarId);

            if (car == null)
            {
                return BadRequest(new { error = "Invalid CarId. Car does not exist." });
            }

            if (car.CarMaintenances == null)
            {
                car.CarMaintenances = new List<CarMaintenance>();
            }
            
            var carMaintenance = new CarMaintenance
            {
                CarId = carMaintenanceDTO.CarId,
                ServiceDate = carMaintenanceDTO.ServiceDate,
                Description = carMaintenanceDTO.Description,
                Cost = carMaintenanceDTO.Cost,
                Car = car
            };

            car.CarMaintenances.Add(carMaintenance);

            _context.CarMaintenances.Add(carMaintenance);
            await _context.SaveChangesAsync();

            carMaintenanceDTO.CarMaintenanceId = carMaintenance.CarMaintenanceId;

            return CreatedAtAction("GetCarMaintenance", new { id = carMaintenance.CarMaintenanceId }, carMaintenance);
        }

        // DELETE: api/CarMaintenance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarMaintenance(int id)
        {
            var maintenance = await _context.CarMaintenances.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            _context.CarMaintenances.Remove(maintenance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarMaintenanceExists(int id)
        {
            return _context.CarMaintenances.Any(e => e.CarMaintenanceId == id);
        }
    }

}