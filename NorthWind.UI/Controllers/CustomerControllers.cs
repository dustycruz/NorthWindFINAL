using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<IActionResult> Index()
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode) return View(new List<CustomerViewModel>());
            var json = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(json);
            return View(customers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            AddAuthHeader();
            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode) return View(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var json = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerViewModel>(json);
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerViewModel model)
        {
            AddAuthHeader();
            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode) return View(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            AddAuthHeader();
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }

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
