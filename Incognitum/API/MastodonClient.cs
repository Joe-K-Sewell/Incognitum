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
        private struct ApiResult<T>
        {
            internal T deserialized;
            internal Response response;
        }

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
        private async Task<ApiResult<T>> ApiCallAsync<T>(Request request)
        {
            var response = await _connection.SendAsync(request);
            return new ApiResult<T>()
            {
                deserialized = JsonConvert.DeserializeObject<T>(response.Content),
                response = response
            };
        }

        private async Task<MastodonPage<T>> ApiCallPageAsync<T>(Request request)
        {
            var response = await _connection.SendAsync(request);
            return new MastodonPage<T>(response);
        }

        /// <summary>
        /// Gets the instance information.
        /// </summary>
        /// <returns>information about the connected instance</returns>
        public async Task<MastodonInstance> GetInstanceInformationAsync()
        {
            var result = await ApiCallAsync<MastodonInstance>(
                new Request
                (
                    Verb.GetPublic,
                    "/api/v1/instance",
                    null,
                    new Dictionary<string, string>()
                ));
            return result.deserialized;
        }

        /// <summary>
        /// Gets a page of the public timeline.
        /// </summary>
        /// <param name="localOnly">Optional. If true, only return statuses from the queried instance</param>
        /// <param name="maxId">Optional. If provided, only return statuses with IDs not greater than this one.</param>
        /// <param name="sinceId">Optional. If provided, only return statuses with IDs greater than this one.</param>
        /// <param name="limit">Optional, defaults to 20. If provided, maximum number of statuses to get. Cannot specify greater than 40.</param>
        /// <returns></returns>
        public async Task<MastodonPage<MastodonStatus>> GetPublicTimelineAsync(
            Boolean? localOnly = null, 
            MastodonStatusId? maxId = null,
            MastodonStatusId? sinceId = null,
            UInt32? limit = null)
        {
            var parameters = new Dictionary<string, string>();

            parameters.AddStringIfHasValue("local", localOnly);
            parameters.AddStringIfHasValue("max_id", maxId);
            parameters.AddStringIfHasValue("since_id", sinceId);
            parameters.AddStringIfHasValue("limit", limit);
            
            return await ApiCallPageAsync<MastodonStatus>(
                new Request
                (
                    Verb.GetPublic,
                    "/api/v1/timelines/public",
                    null,
                    parameters
                ));
        }
    }
}
