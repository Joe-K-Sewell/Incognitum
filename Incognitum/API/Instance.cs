using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Incognitum.API
{
    public class Instance
    {
        [JsonProperty(PropertyName = "uri")]
        public String UriString { get; set; }

        [JsonIgnore]
        public Uri Uri { get { return new UriBuilder("https", UriString).Uri; } }

        [JsonProperty(PropertyName = "title")]
        public String Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }

        [JsonProperty(PropertyName = "email")]
        public String EMail { get; set; }

        [JsonExtensionData]
        internal Dictionary<String, JToken> NonSpecDataRaw;

        [JsonIgnore]
        public Dictionary<String, String> NonSpecData
        {
            get
            {
                return NonSpecDataRaw.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            }
        }
        
        public override string ToString()
        {
            return 
                $"Mastodon Instance {{uri=\"{UriString}\",title=\"{Title}\",description=\"{Description}\",email:\"{EMail}\"}}";
        }
    }
}
