using CarRental.Shared.Models;
using CarRental.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CarRental.MVC.Models;

namespace CarRental.Client.Controllers
{
    public class CarsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync("Cars");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var cars = await response.Content.ReadFromJsonAsync<List<CarDTO>>();
            return View(cars);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarDTO carDTO)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            // car.Rentals = new List<Rental>();
            // car.CarMaintenances = new List<CarMaintenance>();
            var response = await client.PostAsJsonAsync("Cars", carDTO);

            if (!response.IsSuccessStatusCode)
            {
                return View(carDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Cars/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var carDTO = await response.Content.ReadFromJsonAsync<CarDTO>();
            return View(carDTO);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarDTO carDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(carDTO);
            }

            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.PutAsJsonAsync($"Cars/{id}", carDTO);

            if (!response.IsSuccessStatusCode)
            {
                return View(carDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Cars/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var car = await response.Content.ReadFromJsonAsync<CarDTO>();
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.DeleteAsync($"Cars/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
