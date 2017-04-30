using System;
using System.Net.Http;

using Incognitum.Helpers;
using System.Threading.Tasks;

namespace Incognitum.Connections
{
    /// <summary>
    /// Represents a connection via HTTPS to a Mastodon instance.
    /// </summary>
    public sealed class HttpsConnection : IConnection
    {
        private HttpClient _client;

        /// <summary>
        /// Creates an <see cref="HttpsConnection"/> to the Mastodon instance
        /// specified by <paramref name="hostName"/>.
        /// </summary>
        /// <param name="hostName">host name of the Mastodon instance</param>
        public HttpsConnection(String hostName)
        {
            Validations.ParameterIsNotNullOrWhiteSpace(nameof(hostName), hostName);

            _client = new HttpClient();
            const string protocol = "https://";
            if (hostName.StartsWith(protocol, StringComparison.OrdinalIgnoreCase))
            {
                hostName = hostName.Substring(protocol.Length);
            }
            if (hostName.EndsWith("/"))
            {
                hostName = hostName.Substring(0, hostName.Length - 1);
            }
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException($"Unrecognized hostname form: {hostName}", nameof(hostName));
            }

            _client.BaseAddress = new UriBuilder(protocol, hostName).Uri;
        }

        /// <summary> The URI pointing to the Mastodon instance. </summary>
        public Uri InstanceUri { get { return _client.BaseAddress; } }

        public async Task<Response> SendAsync(Request request)
        {
            using (var content = new FormUrlEncodedContent(request.Arguments))
            {
                using (var response = await _client.PostAsync(request.Path, content))
                {
                    response.EnsureSuccessStatusCode();
                    return new Response(await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
