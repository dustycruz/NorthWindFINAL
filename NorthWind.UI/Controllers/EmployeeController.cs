using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthWind.UI.Models;
using NorthWind.UI.Models.DTO;
using System.Net.Http.Headers;
using System.Text;

namespace NorthWind.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _accessor;
        private readonly string _baseUrl = "http://localhost:5155/api/Employee";

        public EmployeeController(IHttpClientFactory factory, IHttpContextAccessor accessor)
        {
            _client = factory.CreateClient();
            _accessor = accessor;
        }

        private void AddAuth()
        {
            var token = _accessor.HttpContext?.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<IActionResult> Index()
        {
            AddAuth();
            var res = await _client.GetAsync(_baseUrl);
            if (!res.IsSuccessStatusCode) return View(new List<EmployeeDto>());
            var json = await res.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<EmployeeDto>>(json);
            return View(data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto model)
        {
            AddAuth();
            var json = JsonConvert.SerializeObject(model);
            var res = await _client.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            if (!res.IsSuccessStatusCode) { ViewBag.Error = "Create failed"; return View(model); }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            AddAuth();
            var res = await _client.GetAsync($"{_baseUrl}/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();
            var json = await res.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<EmployeeDto>(json);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeDto model)
        {
            AddAuth();
            var json = JsonConvert.SerializeObject(model);
            var res = await _client.PutAsync($"{_baseUrl}/{model.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
            if (!res.IsSuccessStatusCode) { ViewBag.Error = "Update failed"; return View(model); }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            AddAuth();
            await _client.DeleteAsync($"{_baseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
