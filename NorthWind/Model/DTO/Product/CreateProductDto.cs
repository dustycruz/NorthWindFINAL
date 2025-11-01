using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Product
{
    public class CreateProductDto
    {
        [Required, StringLength(100)]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        [StringLength(50)]
        public string QuantityPerUnit { get; set; }

        [Range(0, 999999)]
        public decimal UnitPrice { get; set; }

        [Range(0, 32767)]
        public short UnitsInStock { get; set; }

        [Range(0, 32767)]
        public short UnitsOnOrder { get; set; }

        [Range(0, 32767)]
        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
    }
}
