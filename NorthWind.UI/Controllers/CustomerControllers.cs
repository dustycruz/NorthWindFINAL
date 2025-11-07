using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Northwind.DTO.Customer;
using NorthWind.UI.Filters;
using NorthWind.UI.Models;
using NorthWind.UI.Models.DTO;
using System.Net.Http.Headers;
using System.Text;

namespace NorthWind.UI.Controllers
{
    [AuthorizeUI] // Require login
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _baseUrl = "http://localhost:5155/api/Customer";

        public CustomerController(IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = factory.CreateClient();
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddAuthHeader()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // ------------------------- INDEX -------------------------
        public async Task<IActionResult> Index()
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode) return View(new List<CustomerViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(json);
            return View(customers);
        }

        // ------------------------- CREATE -------------------------
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            AddAuthHeader();
            var dto = new CreateCustomerDto
            {
                CompanyName = model.CompanyName,
                ContactName = model.ContactName,
                ContactTitle = model.ContactTitle,
                Address = model.Address,
                City = model.City,
                Region = model.Region,
                PostalCode = model.PostalCode,
                Country = model.Country,
                Phone = model.Phone,
                Fax = model.Fax
            };


            var json = JsonConvert.SerializeObject(dto);
            var response = await _httpClient.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"Create failed: {error}";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ------------------------- EDIT -------------------------
        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var customerVm = JsonConvert.DeserializeObject<CustomerViewModel>(json);

            var dto = new CustomerDto
            {
                CustomerId = customerVm.CustomerId,
                CompanyName = customerVm.CompanyName,
                ContactName = customerVm.ContactName,
                ContactTitle = customerVm.ContactTitle,
                City = customerVm.City
            };

            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            AddAuthHeader();
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{dto.CustomerId}", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"Failed to update: {error}";
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }


        // ------------------------- DELETE -------------------------
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            AddAuthHeader();
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Delete failed!";
            }
            return RedirectToAction(nameof(Index));
        }

        // ------------------------- DETAILS -------------------------
        public async Task<IActionResult> Details(int id)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerViewModel>(json);
            return View(customer);
        }
    }
}
