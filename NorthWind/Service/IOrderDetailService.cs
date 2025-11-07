using Northwind.DTO.Order;


namespace NorthWind.Service
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailDto>> GetAllAsync();
        Task<OrderDetailDto?> GetByIdAsync(int orderId, int productId);
        Task<OrderDetailDto> CreateAsync(CreateOrderDetailDto dto);
        Task<OrderDetailDto?> UpdateAsync(int orderId, int productId, UpdateOrderDetailDto dto);
        Task<bool> DeleteAsync(int orderId, int productId);
    }
}
