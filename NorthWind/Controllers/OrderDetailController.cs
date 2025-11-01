using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.DTO.Order;
using Northwind.Model.Domain;
using Northwind.Repositories;

namespace Northwind.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IGenericRepository<OrderDetail> _repository;
        private readonly IMapper _mapper;

        public OrderDetailController(IGenericRepository<OrderDetail> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(_mapper.Map<List<OrderDetailDto>>(await _repository.GetAllAsync()));

        [HttpGet("{orderId}/{productId}")]
        public async Task<IActionResult> GetById(int orderId, int productId)
        {
            var entity = await _repository.FindAsync(od => od.OrderId == orderId && od.ProductId == productId);
            var orderDetail = entity.FirstOrDefault();
            if (orderDetail == null) return NotFound();
            return Ok(_mapper.Map<OrderDetailDto>(orderDetail));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDetailDto dto)
        {
            var entity = _mapper.Map<OrderDetail>(dto);
            await _repository.AddAsync(entity);
            return Ok(_mapper.Map<OrderDetailDto>(entity));
        }

        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> Update(int orderId, int productId, OrderDetailDto dto)
        {
            var existingList = await _repository.FindAsync(od => od.OrderId == orderId && od.ProductId == productId);
            var existing = existingList.FirstOrDefault();
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
            return Ok(_mapper.Map<OrderDetailDto>(existing));
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> Delete(int orderId, int productId)
        {
            var existingList = await _repository.FindAsync(od => od.OrderId == orderId && od.ProductId == productId);
            var existing = existingList.FirstOrDefault();
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(existing);
            return NoContent();
        }
    }
}
