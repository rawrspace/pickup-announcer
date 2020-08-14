using Newtonsoft.Json;
using PickupAnnouncer.Models.DTO;
using System.Collections.Generic;

namespace PickupAnnouncer.Models
{
    public class PickupNotice
    {
        [JsonProperty("car")]
        public string Car { get; set; }
        [JsonProperty("cone")]
        public string Cone { get; set; }
        [JsonProperty("students")]
        public List<StudentDTO> Students;
    }
}
