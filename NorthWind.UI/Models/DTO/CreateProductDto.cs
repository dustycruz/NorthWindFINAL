namespace Northwind.DTO.Product
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}
