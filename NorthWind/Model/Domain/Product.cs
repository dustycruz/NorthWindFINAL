using System.Collections.Generic;

namespace Northwind.Model.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        // 🔹 Add this line if missing
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
