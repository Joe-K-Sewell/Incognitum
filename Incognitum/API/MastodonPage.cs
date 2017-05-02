using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;

using Newtonsoft.Json;

using Incognitum.Connections;

namespace Incognitum.API
{
    /// <summary>
    /// A subset of an ordered list of result objects.
    /// </summary>
    public class MastodonPage<T>
    {
        /// <summary> The results on this page. </summary>
        public T[] Results { get; private set; }

        internal MastodonPage(Response response)
        {
            Results = JsonConvert.DeserializeObject<T[]>(response.Content);
        }
    }
}
