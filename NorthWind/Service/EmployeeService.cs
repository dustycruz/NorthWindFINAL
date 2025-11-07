using AutoMapper;
using Northwind.DTO.Employee;
using Northwind.Model.Domain;
using NorthWind.Repositories;

namespace NorthWind.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _repo;
        private readonly IMapper _mapper;

        public EmployeeService(IGenericRepository<Employee> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repo.GetAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var entity = _mapper.Map<Employee>(dto);
            var created = await _repo.AddAsync(entity);
            return _mapper.Map<EmployeeDto>(created);
        }

        public async Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null)
                return null;

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);
            return _mapper.Map<EmployeeDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null)
                return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
