using System.Diagnostics;
using CarRental.MVC.Models;
using CarRental.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.MVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync("Customers");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var customers = await response.Content.ReadFromJsonAsync<List<CustomerDTO>>();
            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Customers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var customer = await response.Content.ReadFromJsonAsync<CustomerDTO>();
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerDTO customerDTO)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.PostAsJsonAsync("Customers", customerDTO);

            if (!response.IsSuccessStatusCode)
            {
                return View(customerDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Customers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var customer = await response.Content.ReadFromJsonAsync<CustomerDTO>();
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerDTO customerDTO)
        {
            if (id != customerDTO.CustomerId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(customerDTO);
            }

            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.PutAsJsonAsync($"Customers/{id}", customerDTO);

            if (!response.IsSuccessStatusCode)
            {
                return View(customerDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.GetAsync($"Customers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var customer = await response.Content.ReadFromJsonAsync<CustomerDTO>();
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CarRentalApi");
            var response = await client.DeleteAsync($"Customers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}