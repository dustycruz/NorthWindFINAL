using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Order
{
    public class CreateOrderDetailDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
    }

    public class UpdateOrderDetailDto : CreateOrderDetailDto { }
}
