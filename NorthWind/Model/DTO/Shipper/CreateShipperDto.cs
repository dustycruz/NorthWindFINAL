using Northwind.DTO.Order;
using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Shipper
{
    public class CreateShipperDto
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
    public class UpdateShipperDto : CreateShipperDto { }
}
