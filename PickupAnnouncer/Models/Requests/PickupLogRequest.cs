using Newtonsoft.Json;
using System;

namespace PickupAnnouncer.Models.Requests
{
    public class PickupLogRequest
    {
        [JsonProperty("startOfDayUTC")]
        public DateTimeOffset StartOfDayUTC { get; set; }
    }
}
