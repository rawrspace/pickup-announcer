using Newtonsoft.Json;

namespace PickupAnnouncer.Models.DTO
{
    public class StudentDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("teacher")]
        public string Teacher { get; set; }
        [JsonProperty("gradeLevel")]
        public int GradeLevel { get; set; }
    }
}
