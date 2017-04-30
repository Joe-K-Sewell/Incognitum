using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace Incognitum.API
{
    public class Instance
    {
        [JsonProperty(PropertyName = "uri")]
        public String UriString { get; set; }

        [JsonProperty(PropertyName = "title")]
        public String Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }

        [JsonProperty(PropertyName = "email")]
        public String EMail { get; set; }

        public override string ToString()
        {
            return 
                $"Mastodon Instance {Title} @ {UriString}{Environment.NewLine}{Description}{Environment.NewLine}Contact: {EMail}";
        }
    }
}
