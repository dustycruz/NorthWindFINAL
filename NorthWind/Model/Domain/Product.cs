using System.Collections.Generic;

namespace Northwind.Model.Domain
{
    public class Product
    {
        public int ProductId { get; set; } // primary key
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool Discontinued { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
