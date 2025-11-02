using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Supplier
{
    public class CreateSupplierDto
    {
        [Required, StringLength(100)]
        public string CompanyName { get; set; }

        [Required, StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(50)]
        public string ContactTitle { get; set; }

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
        public string Phone { get; set; }

        [Phone]
        public string Fax { get; set; }

        [Url]
        public string HomePage { get; set; }
    }
}
