using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Order
{
    public class CreateOrderDto
    {
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RequiredDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ShippedDate { get; set; }

        public int? ShipViaId { get; set; }

        [Range(0, 999999)]
        public decimal? Freight { get; set; }

        [StringLength(100)]
        public string ShipName { get; set; }

        [StringLength(200)]
        public string ShipAddress { get; set; }

        public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
    }
}
