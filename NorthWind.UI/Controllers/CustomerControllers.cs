using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using NorthWind.UI.Models.DTO;
using Northwind.DTO.Customer;

namespace NorthWind.UI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (token == null)
                return RedirectToAction("Login", "Account");

            var client = httpClientFactory.CreateClient("NorthwindAPI");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("Customer"); // ✅ correct endpoint

            if (!response.IsSuccessStatusCode)
                return View(new List<CustomerDto>()); // or return error view

            var customers = await response.Content.ReadFromJsonAsync<List<CustomerDto>>();
            return View(customers);
        }


        [HttpPost]
        public async Task<IActionResult> Add(CreateCustomerDto model)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = httpClientFactory.CreateClient("NorthwindAPI");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("Customer", model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Customer");

            ViewBag.Error = $"Error: {response.StatusCode}";
            return View(model);
        }




    }
}
