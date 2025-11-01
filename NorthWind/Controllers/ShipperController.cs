using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Model.Domain;
using Northwind.Repositories;
using Northwind.DTO.Shipper;

namespace Northwind.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShipperController : ControllerBase
    {
        private readonly IGenericRepository<Shipper> _repository;
        private readonly IMapper _mapper;

        public ShipperController(IGenericRepository<Shipper> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(_mapper.Map<List<ShipperDto>>(await _repository.GetAllAsync()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<ShipperDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShipperDto dto)
        {
            var entity = _mapper.Map<Shipper>(dto);
            await _repository.AddAsync(entity);
            return Ok(_mapper.Map<ShipperDto>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ShipperDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
            return Ok(_mapper.Map<ShipperDto>(existing));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
