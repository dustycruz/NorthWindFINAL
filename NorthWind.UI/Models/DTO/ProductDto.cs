namespace NorthWind.UI.Models.DTO
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool Discontinued { get; set; }
    }
}
