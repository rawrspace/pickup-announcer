﻿using Newtonsoft.Json;

namespace PickupAnnouncer.Models.Responses
{
    public class StatusResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
