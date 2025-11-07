using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.DTO.Product;
using Northwind.Model.Domain;
using NorthWind.DTOs;

using NorthWind.Service;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthWind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // require authentication (can remove temporarily while debugging)
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var products = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var p = await _service.GetAsync(id);
            if (p == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(p));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = _mapper.Map<Product>(dto);
            var created = await _service.AddAsync(product);
            var outDto = _mapper.Map<ProductDto>(created);
            return CreatedAtAction(nameof(Get), new { id = outDto.ProductId }, outDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreateProductDto dto)
        {
            var existing = await _service.GetAsync(id);
            if (existing == null) return NotFound();
            _mapper.Map(dto, existing);
            await _service.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetAsync(id);
            if (existing == null) return NotFound();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
