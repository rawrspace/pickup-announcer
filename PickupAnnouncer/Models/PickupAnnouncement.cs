using Newtonsoft.Json;
using System.Collections.Generic;

namespace PickupAnnouncer.Models
{
    public class PickupAnnouncement
    {
        [JsonProperty("car")]
        public string Car { get; set; }
        [JsonProperty("cone")]
        public string Cone { get; set; }
        [JsonProperty("studentDetails")]
        public List<StudentDetails> StudentDetails;
    }
}
