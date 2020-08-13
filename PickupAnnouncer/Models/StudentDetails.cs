using Newtonsoft.Json;

namespace PickupAnnouncer.Models
{
    public class StudentDetails
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("teacher")]
        public string Teacher { get; set; }
        [JsonProperty("gradeLevel")]
        public int GradeLevel { get; set; }
    }
}
