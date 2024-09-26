using System.Collections.Generic;
using Newtonsoft.Json;

namespace Member.Models.Member
{
    public class MemberGetListResp
    {
        //[JsonProperty("pk")]
        public string? Pk { get; set; }

        //[JsonProperty("id")]
        public string? Id { get; set; }

        //[JsonProperty("name")]
        public string? Name { get; set; }

        //[JsonProperty("gender")]
        public string? Gender { get; set; }

        //[JsonProperty("enable")]
        public string? Enable { get; set; }
    }

}


