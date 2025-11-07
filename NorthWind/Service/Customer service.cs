using AutoMapper;
using Northwind.Model.Domain;
using NorthWind.Repositories;
using Northwind.DTO.Customer;

namespace NorthWind.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _repo;
        private readonly IMapper _mapper;

        public CustomerService(IGenericRepository<Customer> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _repo.GetAsync(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
        {
            var entity = _mapper.Map<Customer>(dto);
            var created = await _repo.AddAsync(entity);
            return _mapper.Map<CustomerDto>(created);
        }

        public async Task<CustomerDto?> UpdateAsync(int id, UpdateCustomerDto dto)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);
            return _mapper.Map<CustomerDto>(existing);
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
