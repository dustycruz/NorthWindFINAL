using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Order
{
    public class CreateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int ShipperId { get; set; }
    }

    public class UpdateOrderDto : CreateOrderDto { }
}
