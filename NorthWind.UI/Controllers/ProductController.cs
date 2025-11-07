using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Northwind.DTO.Product;
using NorthWind.UI.Filters;
using NorthWind.UI.Models;
using NorthWind.UI.Models.DTO;
using System.Net.Http.Headers;
using System.Text;

namespace NorthWind.UI.Controllers
{
    [AuthorizeUI]
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5155/api/Product";

        public ProductsController(IHttpClientFactory factory)
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
            if (!response.IsSuccessStatusCode) return View(new List<ProductViewModel>());
            var json = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductViewModel>>(json);
            return View(products);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            AddAuthHeader();
            var json = JsonConvert.SerializeObject(dto);
            var response = await _httpClient.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode) return View(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductViewModel>(json);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateProductDto dto)
        {
            AddAuthHeader();
            var json = JsonConvert.SerializeObject(dto);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode) return View(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            AddAuthHeader();
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
