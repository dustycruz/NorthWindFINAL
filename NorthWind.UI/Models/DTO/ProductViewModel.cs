namespace NorthWind.UI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}
