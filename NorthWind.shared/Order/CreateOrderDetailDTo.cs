using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Order
{
    public class CreateOrderDetailDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(0, 999999)]
        public decimal UnitPrice { get; set; }

        [Range(1, 1000)]
        public short Quantity { get; set; }

        [Range(0, 1)]
        public float Discount { get; set; }
    }
}
