using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Data; // Your DbContext namespace
using Northwind.DTO.Customer; // Your CreateCustomerDto namespace
using Northwind.Model.Domain;


namespace NorthWind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // ✅ requires token
    public class CustomerController : ControllerBase
    {
        private readonly NorthwindDbContext _context;

        public CustomerController(NorthwindDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult Create(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                CompanyName = dto.CompanyName,
                ContactName = dto.ContactName,
                ContactTitle = dto.ContactTitle,
                Address = dto.Address,
                City = dto.City,
                Region = dto.Region,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
                Phone = dto.Phone,
                Fax = dto.Fax
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok(customer);
        }
    }
}
