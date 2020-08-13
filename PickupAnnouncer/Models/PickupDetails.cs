using Newtonsoft.Json;

namespace PickupAnnouncer.Models
{
    public class PickupDetails
    {
        [JsonProperty("car")]
        public string Car { get; set; }
        [JsonProperty("cone")]
        public string Cone { get; set; }
    }
}
