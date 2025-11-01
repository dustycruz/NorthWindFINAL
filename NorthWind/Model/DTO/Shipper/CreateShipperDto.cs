using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Shipper
{
    public class CreateShipperDto
    {
        [Required, StringLength(100)]
        public string CompanyName { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
