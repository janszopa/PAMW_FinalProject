using System.Diagnostics;
using CarRental.MVC.Models;
using CarRental.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.MVC.Controllers
{
    public class CarMaintenancesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarMaintenancesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //TODO: dodac widoki Create, Edit, Delete z copilota github, pozmienaic typy tu w kontrolerze
        //zastanowić sie nad logika, czy w Index wyswietlamy info o samochodzie, czy np. przycisk details do teog
        //tak samo czy przycisk details w tabeli Cars, pokazujacy liste maintenance
        

        // GET: CarMaintenance
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync("CarMaintenance");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var maintenances = await response.Content.ReadFromJsonAsync<List<CarMaintenanceDTO>>();
            return View(maintenances);
        }

        // GET: CarMaintenance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarMaintenance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCarMaintenanceDTO carMaintenanceDTO)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.PostAsJsonAsync("CarMaintenance", carMaintenanceDTO);

            // if (!response.IsSuccessStatusCode)
            // {
            //     return View(carMaintenanceDTO);
            // }

            return RedirectToAction(nameof(Index));
        }

        // GET: CarMaintenance/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"CarMaintenance/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var carMaintenanceDTO = await response.Content.ReadFromJsonAsync<PostCarMaintenanceDTO>();
            return View(carMaintenanceDTO);
        }

        // POST: CarMaintenance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostCarMaintenanceDTO carMaintenanceDTO)
        {
            if (id != carMaintenanceDTO.CarMaintenanceId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(carMaintenanceDTO);
            }

            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.PutAsJsonAsync($"CarMaintenance/{id}", carMaintenanceDTO);

            if (!response.IsSuccessStatusCode)
            {
                return View(carMaintenanceDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CarMaintenance/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"CarMaintenance/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var carMaintenanceDTO = await response.Content.ReadFromJsonAsync<CarMaintenanceDTO>();
            return View(carMaintenanceDTO);
        }

        // POST: CarMaintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.DeleteAsync($"CarMaintenance/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}