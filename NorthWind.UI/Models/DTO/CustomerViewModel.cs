using Newtonsoft.Json;

namespace NorthWind.UI.Models
{
    public class CustomerViewModel
    {
        [JsonProperty("id")]

        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }      // required
        public string City { get; set; }
        public string Region { get; set; }       // required
        public string PostalCode { get; set; }   // required
        public string Country { get; set; }      // required
        public string Phone { get; set; }        // required
        public string Fax { get; set; }          // required
    }
}
