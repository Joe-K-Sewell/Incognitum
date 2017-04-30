using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Incognitum.Authentication;
using Incognitum.Connections;
using Incognitum.Helpers;

namespace Incognitum.API
{
    /// <summary>
    /// Represents an interface to a particular Mastodon instance.
    /// This is a thin wrapper over the Mastodon API.
    /// </summary>
    public class MastodonClient
    {
        private readonly Connection _connection;

        /// <summary>
        /// Creates a MastodonClient using a specific connection implementation.
        /// </summary>
        /// <param name="connection">the connection logic for a given Mastodon instance</param>
        internal MastodonClient(Connection connection)
        {
            Validations.ParameterIsNotDefaultValue(nameof(connection), connection);
            _connection = connection;
        }

        /// <summary>
        /// Creates a MastodonClient to the given instance.
        /// </summary>
        /// <param name="instanceHostName">host name of the instance (e.g., "mastodon.social")</param>
        public MastodonClient(String instanceHostName) : this(new HttpsConnection(instanceHostName))
        {

        }

        /// <summary>
        /// Make a call to the API, and deserialize the result to a given type.
        /// </summary>
        /// <typeparam name="T">the API type that is expected as a return value</typeparam>
        /// <param name="request">the request to make to the API</param>
        /// <returns>the deserialized API object</returns>
        private async Task<T> ApiCallAsync<T>(Request request)
        {
            var response = await _connection.SendAsync(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        /// <summary>
        /// Gets the instance information.
        /// </summary>
        /// <returns>information about the connected instance</returns>
        public async Task<MastodonInstance> GetInstanceInformationAsync()
        {
            return await 
                ApiCallAsync<MastodonInstance>(new Request
                (
                    Verb.GetPublic,
                    "/api/v1/instance",
                    default(AuthToken),
                    new Dictionary<string, string>()
                ));
        }
    }
}
