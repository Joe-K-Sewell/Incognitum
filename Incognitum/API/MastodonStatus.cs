using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Incognitum.API
{
    /// <summary>
    /// A unique identifier of a <seealso cref="MastodonStatus"/>.
    /// </summary>
    public struct MastodonStatusId
    {
        /// <summary>
        /// The actual numeric identifier returned by the instance.
        /// </summary>
        public UInt64 Value;
    }

    /// <summary>
    /// A status (post) on Mastodon.
    /// </summary>
    public class MastodonStatus
    {
        /// <summary> Unique identifier for the status </summary>
        public MastodonStatusId Id { get; internal set; }

        /// <summary> Used for Json.NET serialization </summary>
        [JsonProperty(PropertyName = "id")]
        internal UInt64 RawId { set { Id = new MastodonStatusId() { Value = value }; } }
    }
}
