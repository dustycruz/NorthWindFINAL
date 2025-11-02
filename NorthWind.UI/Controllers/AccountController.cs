using Microsoft.AspNetCore.Mvc;
using Northwind.UI.Models.DTO;
using NorthWind.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NorthWind.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)

        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:5155/api/Auth/login"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponse = await client.SendAsync(httpRequestMessage);

            if (!httpResponse.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid Credentials";
                return View();
            }

            var loginResult = await httpResponse.Content.ReadFromJsonAsync<LoginResponseDto>();

            // ✅ STORE TOKEN IN SESSION
            HttpContext.Session.SetString("JWToken", loginResult.Token);

            // ✅ REDIRECT
            return RedirectToAction("Index", "Customer");
        }
    }
}
