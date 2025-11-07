using AutoMapper;
using Northwind.Model.Domain;
using NorthWind.Repositories;
using Northwind.DTO.Order;

namespace NorthWind.Service
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _repo;
        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _repo.GetAsync(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var entity = _mapper.Map<Order>(dto);
            var created = await _repo.AddAsync(entity);
            return _mapper.Map<OrderDto>(created);
        }

        public async Task<OrderDto?> UpdateAsync(int id, UpdateOrderDto dto)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);
            return _mapper.Map<OrderDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
