using System;
using System.Net.Http;

using Incognitum.Helpers;

namespace Incognitum.Connections
{
    /// <summary>
    /// Represents a connection via HTTPS to a Mastodon instance.
    /// </summary>
    public class HttpsConnection : IConnection
    {
        private HttpClient _client;

        /// <summary>
        /// Creates an <see cref="HttpsConnection"/> to the Mastodon instance
        /// specified by <paramref name="hostName"/>.
        /// </summary>
        /// <param name="hostName"></param>
        public HttpsConnection(String hostName)
        {
            Validations.ParameterIsNotNullOrWhiteSpace(nameof(hostName), hostName);

            _client = new HttpClient();
            const string protocol = "https://";
            if (hostName.StartsWith(protocol, StringComparison.OrdinalIgnoreCase))
            {
                _client.BaseAddress = new Uri(hostName);
            }
            else
            {
                _client.BaseAddress = new UriBuilder(protocol, hostName).Uri;
            }
        }
    }
}
