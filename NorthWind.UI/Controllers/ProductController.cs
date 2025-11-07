using Microsoft.AspNetCore.Mvc;
using Northwind.DTO.Product;
using NorthWind.UI.Models.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NorthWind.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _http;

        public ProductsController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("NorthWindAPI");
        }

        public async Task<IActionResult> Index()
        {
            var products = await _http.GetFromJsonAsync<IEnumerable<ProductDto>>("/api/products");
            return View(products);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var res = await _http.PostAsJsonAsync("/api/products", dto);
            if (!res.IsSuccessStatusCode) return View("Error");
            return RedirectToAction(nameof(Index));
        }

        // Edit and Delete actions can be added similarly (call PUT /api/products/{id} and DELETE /api/products/{id})
    }
}
