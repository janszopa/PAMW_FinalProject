using System.Diagnostics;
using CarRental.MVC.Models;
using CarRental.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.MVC.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RentalsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Rentals
        public async Task<IActionResult> Index(string carBrand, string carModel, string customerFirstName, string customerLastName)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync("Rentals");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var rentals = await response.Content.ReadFromJsonAsync<List<RentalDTO>>();

            if (!string.IsNullOrEmpty(carBrand))
            {
                rentals = rentals.Where(r => r.CarBrand.Contains(carBrand, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(carModel))
            {
                rentals = rentals.Where(r => r.CarModel.Contains(carModel, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(customerFirstName))
            {
                rentals = rentals.Where(r => r.CustomerFirstName.Contains(customerFirstName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(customerLastName))
            {
                rentals = rentals.Where(r => r.CustomerLastName.Contains(customerLastName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Rentals/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var rental = await response.Content.ReadFromJsonAsync<RentalDTO>();
            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalDTO rentalDTO)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.PostAsJsonAsync("Rentals", rentalDTO);

            if (!response.IsSuccessStatusCode)
            {
                return View(rentalDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Rentals/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var rental = await response.Content.ReadFromJsonAsync<RentalDTO>();
            return View(rental);
        }

        // POST: Rentals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RentalDTO rentalDTO)
        {
            if (id != rentalDTO.RentalId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(rentalDTO);
            }

            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.PutAsJsonAsync($"Rentals/{id}", rentalDTO);

            if (!response.IsSuccessStatusCode)
            {
                return View(rentalDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Rentals/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var rental = await response.Content.ReadFromJsonAsync<RentalDTO>();
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.DeleteAsync($"Rentals/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}