using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthWind.UI.Filters;
using NorthWind.UI.Models;
using NorthWind.UI.Models.DTO;
using System.Net.Http.Headers;
using System.Text;

namespace NorthWind.UI.Controllers
{
    [AuthorizeUI]
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5155/api/Order";

        public OrderController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        private void AddAuthHeader() =>
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWToken"));

        public async Task<IActionResult> Index()
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode) return View(new List<OrderViewModel>());
            var json = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(json);
            return View(orders);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
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
            var order = JsonConvert.DeserializeObject<OrderViewModel>(json);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OrderViewModel model)
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
            var order = JsonConvert.DeserializeObject<OrderViewModel>(json);
            return View(order);
        }
    }
}
