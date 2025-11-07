using AutoMapper;
using Northwind.DTO.Order;
using Northwind.Model.Domain;
using NorthWind.Repositories;

namespace NorthWind.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IGenericRepository<OrderDetail> _repo;
        private readonly IMapper _mapper;

        public OrderDetailService(IGenericRepository<OrderDetail> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllAsync()
        {
            var details = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDetailDto>>(details);
        }

        public async Task<OrderDetailDto?> GetByIdAsync(int orderId, int productId)
        {
            var all = await _repo.GetAllAsync();
            var entity = all.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            return entity == null ? null : _mapper.Map<OrderDetailDto>(entity);
        }

        public async Task<OrderDetailDto> CreateAsync(CreateOrderDetailDto dto)
        {
            var entity = _mapper.Map<OrderDetail>(dto);
            var created = await _repo.AddAsync(entity);
            return _mapper.Map<OrderDetailDto>(created);
        }

        public async Task<OrderDetailDto?> UpdateAsync(int orderId, int productId, UpdateOrderDetailDto dto)
        {
            var all = await _repo.GetAllAsync();
            var existing = all.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);
            return _mapper.Map<OrderDetailDto>(existing);
        }

        public async Task<bool> DeleteAsync(int orderId, int productId)
        {
            var all = await _repo.GetAllAsync();
            var existing = all.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (existing == null) return false;

            // call the repository delete that accepts the entity (common signature)
            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}
