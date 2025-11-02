namespace NorthWind.UI.Models.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }   // Match this to your API model
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
    }
}
