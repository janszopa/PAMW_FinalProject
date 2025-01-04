using System.Diagnostics;
using CarRental.MVC.Models;
using CarRental.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.MVC.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync("Payments");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var payments = await response.Content.ReadFromJsonAsync<List<PaymentDTO>>();
            return View(payments);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Payments/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var payment = await response.Content.ReadFromJsonAsync<PaymentDTO>();
            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentDTO paymentDTO)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.PostAsJsonAsync("Payments", paymentDTO);

            if (!response.IsSuccessStatusCode)
            {
                return View(paymentDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Payments/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var payment = await response.Content.ReadFromJsonAsync<PaymentDTO>();
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentDTO paymentDTO)
        {
            if (id != paymentDTO.PaymentId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(paymentDTO);
            }

            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.PutAsJsonAsync($"Payments/{id}", paymentDTO);

            if (!response.IsSuccessStatusCode)
            {
                return View(paymentDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Payments/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var payment = await response.Content.ReadFromJsonAsync<PaymentDTO>();
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.DeleteAsync($"Payments/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}