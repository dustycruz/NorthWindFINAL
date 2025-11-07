using Northwind.DTO.Shipper;

namespace NorthWind.Service
{
    public interface IShipperService
    {
        Task<IEnumerable<ShipperDto>> GetAllAsync();
        Task<ShipperDto?> GetByIdAsync(int id);
        Task<ShipperDto> CreateAsync(CreateShipperDto dto);
        Task<ShipperDto?> UpdateAsync(int id, UpdateShipperDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
