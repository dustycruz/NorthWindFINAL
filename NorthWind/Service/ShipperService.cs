using AutoMapper;
using Northwind.Model.Domain;
using NorthWind.Repositories;
using Northwind.DTO.Shipper;

namespace NorthWind.Service
{
    public class ShipperService : IShipperService
    {
        private readonly IGenericRepository<Shipper> _repo;
        private readonly IMapper _mapper;

        public ShipperService(IGenericRepository<Shipper> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShipperDto>> GetAllAsync()
        {
            var shippers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ShipperDto>>(shippers);
        }

        public async Task<ShipperDto?> GetByIdAsync(int id)
        {
            var shipper = await _repo.GetAsync(id);
            return shipper == null ? null : _mapper.Map<ShipperDto>(shipper);
        }

        public async Task<ShipperDto> CreateAsync(CreateShipperDto dto)
        {
            var entity = _mapper.Map<Shipper>(dto);
            var created = await _repo.AddAsync(entity);
            return _mapper.Map<ShipperDto>(created);
        }

        public async Task<ShipperDto?> UpdateAsync(int id, UpdateShipperDto dto)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);
            return _mapper.Map<ShipperDto>(existing);
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
