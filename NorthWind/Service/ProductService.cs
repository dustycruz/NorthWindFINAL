using AutoMapper;
using Northwind.Model.Domain;
using NorthWind.Repositories;
using Northwind.DTO.Product;

namespace NorthWind.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repo;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repo.GetAsync(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            var created = await _repo.AddAsync(entity);
            return _mapper.Map<ProductDto>(created);
        }

        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);
            return _mapper.Map<ProductDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }

        Task<IEnumerable<Product>> IProductService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        Task IProductService.DeleteAsync(int id)
        {
            return DeleteAsync(id);
        }
    }
}
