using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Incognitum.API
{
    /// <summary>
    /// Information about a Mastodon instance (server).
    /// </summary>
    /// <remarks>Defined by https://github.com/tootsuite/documentation/blob/master/Using-the-API/API.md#instance </remarks>
    public class MastodonInstance
    {
        /// <summary>
        /// URI of the instance, as reported by the API.
        /// </summary>
        /// <remarks><code>uri</code> in spec</remarks>
        [JsonProperty(PropertyName = "uri")]
        public String UriString { get; set; }

        /// <summary>
        /// Complete URI of the instance.
        /// </summary>
        [JsonIgnore]
        public Uri Uri { get { return new UriBuilder("https", UriString).Uri; } }

        /// <summary>
        /// The instance's title.
        /// </summary>
        /// <remarks><code>title</code> in spec</remarks>
        [JsonProperty(PropertyName = "title")]
        public String Title { get; set; }

        /// <summary>
        /// A description for the instance.
        /// </summary>
        /// <remarks><code>description</code> in spec</remarks>
        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }

        /// <summary>
        /// An email address which can be used to contact the instance administrator.
        /// </summary>
        /// <remarks><code>email</code> in spec</remarks>
        [JsonProperty(PropertyName = "email")]
        public String Email { get; set; }

        /// <summary>
        /// Additional keys deserialized by Json.NET.
        /// </summary>
        [JsonExtensionData]
        internal Dictionary<String, JToken> NonSpecDataRaw;

        /// <summary>
        /// Additional key-value pairs found on the API response
        /// that are not part of the Mastodon spec.
        /// </summary>
        [JsonIgnore]
        public Dictionary<String, String> NonSpecData
        {
            get
            {
                return NonSpecDataRaw.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            }
        }
        
        /// <summary>
        /// Returns a developer-friendly string representing this object.
        /// </summary>
        /// <returns>the string</returns>
        public override string ToString()
        {
            return 
                $"Mastodon Instance {{uri=\"{UriString}\",title=\"{Title}\",description=\"{Description}\",email:\"{Email}\"}}";
        }
    }
}
