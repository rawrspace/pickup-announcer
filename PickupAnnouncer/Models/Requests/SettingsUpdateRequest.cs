using Newtonsoft.Json;

namespace PickupAnnouncer.Models.Requests
{
    public class SettingsUpdateRequest
    {
        [JsonProperty("numberOfCones")]
        public int NumberOfCones { get; set; }
    }
}
