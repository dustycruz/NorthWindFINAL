using System;
using System.Collections.Generic;

namespace Northwind.Model.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ShipperId { get; set; }
        public Shipper Shipper { get; set; }

        // 🔹 Add this line if missing
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
