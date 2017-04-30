using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace Incognitum.API
{
    public class Instance
    {
        [JsonProperty(PropertyName = "uri")]
        public Uri Uri { get; set; }

        [JsonProperty(PropertyName = "title")]
        public String Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }

        [JsonProperty(PropertyName = "email")]
        public String EMail { get; set; }
    }
}
