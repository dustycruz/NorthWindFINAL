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

        // Get all products (entities)
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        // Get single product
        public async Task<Product> GetAsync(int id)
        {
            return await _repo.GetAsync(id);
        }

        // Add new product
        public async Task<Product> AddAsync(Product product)
        {
            return await _repo.AddAsync(product);
        }

        // Update existing product
        public async Task UpdateAsync(Product product)
        {
            await _repo.UpdateAsync(product);
        }

        // Delete product
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
