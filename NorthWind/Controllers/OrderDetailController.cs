using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.DTO.Order;
using NorthWind.Service;

namespace Northwind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _service;

        public OrderDetailController(IOrderDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var details = await _service.GetAllAsync();
            return Ok(details);
        }

        [HttpGet("{orderId}/{productId}")]
        public async Task<IActionResult> Get(int orderId, int productId)
        {
            var detail = await _service.GetByIdAsync(orderId, productId);
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDetailDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { orderId = created.OrderId, productId = created.ProductId }, created);
        }

        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> Update(int orderId, int productId, [FromBody] UpdateOrderDetailDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(orderId, productId, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> Delete(int orderId, int productId)
        {
            var success = await _service.DeleteAsync(orderId, productId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
