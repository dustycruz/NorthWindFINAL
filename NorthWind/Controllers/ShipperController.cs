using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.DTO.Shipper;
using NorthWind.Service;

namespace Northwind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _service;

        public ShipperController(IShipperService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var shippers = await _service.GetAllAsync();
            return Ok(shippers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var shipper = await _service.GetByIdAsync(id);
            if (shipper == null) return NotFound();
            return Ok(shipper);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShipperDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateShipperDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
