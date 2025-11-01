using System;
using System.Collections.Generic;

namespace Northwind.DTO.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; } = new();
    }
}
