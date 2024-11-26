using System.Text.Json.Serialization;

namespace Application.Interfaces.HttpClients.Address
{
    public class AddressResponse
    {
        [JsonPropertyName("streetName")]
        public string StreetName { get; set; }

        [JsonPropertyName("streetNumber")]
        public string StreetNumber { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}
