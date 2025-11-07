using Microsoft.AspNetCore.Mvc;
using Northwind.UI.Models.DTO;
using NorthWind.UI.Models;
using NorthWind.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NorthWind.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:5155/api/Auth/login"),
                Content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }

            var loginResult = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            HttpContext.Session.SetString("JWToken", loginResult.Token);

            return RedirectToAction("Index", "Customer");
        }
    }
}
