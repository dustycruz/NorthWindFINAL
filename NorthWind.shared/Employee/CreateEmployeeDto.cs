using System;
using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Employee
{
    public class CreateEmployeeDto
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [Phone]
        public string HomePhone { get; set; }

        public int? ReportsToId { get; set; }
    }
}
