﻿using Dapper;
using Newtonsoft.Json;

namespace PickupAnnouncer.Models.DAO.Config
{
    [Table("GradeLevel", Schema = "Config")]
    public class GradeLevel : BaseDAO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }
        [JsonProperty("textColor")]
        public string TextColor { get; set; }
    }
}
