using AutoMapper;
using Northwind.Model.Domain;
using NorthWind.Repositories;
using Northwind.DTO.Supplier;

namespace NorthWind.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly IGenericRepository<Supplier> _repo;
        private readonly IMapper _mapper;

        public SupplierService(IGenericRepository<Supplier> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto?> GetByIdAsync(int id)
        {
            var supplier = await _repo.GetAsync(id);
            return supplier == null ? null : _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<SupplierDto> CreateAsync(CreateSupplierDto dto)
        {
            var entity = _mapper.Map<Supplier>(dto);
            var created = await _repo.AddAsync(entity);
            return _mapper.Map<SupplierDto>(created);
        }

        public async Task<SupplierDto?> UpdateAsync(int id, UpdateSupplierDto dto)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);
            return _mapper.Map<SupplierDto>(existing);
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
