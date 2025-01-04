using Microsoft.AspNetCore.Mvc;
using CarRental.Shared.Models;
using CarRental.API.Data;
using Microsoft.EntityFrameworkCore;
using CarRental.Shared.DTO;

namespace CarRental.API.Controller 
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            var payments = await _context.Payments
                .Include(p => p.Rental)
                .Select(p => new PaymentDTO
                {
                    PaymentId = p.PaymentId,
                    RentalId = p.RentalId,
                    Amount = p.Amount,
                    Date = p.Date,
                    Method = p.Method
                })
                .ToListAsync();

            return Ok(payments);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPayment(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Rental)
                .Where(p => p.PaymentId == id)
                .Select(p => new PaymentDTO
                {
                    PaymentId = p.PaymentId,
                    RentalId = p.RentalId,
                    Amount = p.Amount,
                    Date = p.Date,
                    Method = p.Method
                })
                .FirstOrDefaultAsync();

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentDTO paymentDTO)
        {
            if (id != paymentDTO.PaymentId)
            {
                return BadRequest();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            payment.RentalId = paymentDTO.RentalId;
            payment.Amount = paymentDTO.Amount;
            payment.Date = paymentDTO.Date;
            payment.Method = paymentDTO.Method;

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(PaymentDTO paymentDTO)
        {
            var rental = await _context.Rentals.FindAsync(paymentDTO.RentalId);

            if (rental == null)
            {
                return BadRequest(new { error = "Invalid RentalId. Rental does not exist." });
            }

            var payment = new Payment
            {
                RentalId = paymentDTO.RentalId,
                Amount = paymentDTO.Amount,
                Date = paymentDTO.Date,
                Method = paymentDTO.Method,
                Rental = rental
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            paymentDTO.PaymentId = payment.PaymentId;

            return CreatedAtAction("GetPayment", new { id = payment.PaymentId }, paymentDTO);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }

}