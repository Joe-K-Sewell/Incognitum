using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using Incognitum.Authentication;
using Incognitum.API;
using Incognitum.Connections;
using Incognitum.Helpers;
using System.Threading.Tasks;

namespace Incognitum
{
    /// <summary>
    /// Represents an interface to a particular Mastodon instance.
    /// This is a thin wrapper over the Mastodon API.
    /// </summary>
    public class MastodonClient
    {
        private readonly IConnection _connection;

        public MastodonClient(IConnection connection)
        {
            Validations.ParameterIsNotDefaultValue(nameof(connection), connection);
            _connection = connection;
        }

        public MastodonClient(String instanceHostName) : this(new HttpsConnection(instanceHostName))
        {

        }

        private async Task<T> ApiCallAsync<T>(Request request)
        {
            var response = await _connection.SendAsync(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<Instance> GetInstanceInformationAsync()
        {
            return await 
                ApiCallAsync<Instance>(new Request
                (
                    Verb.GetPublic,
                    "api/v1/instance",
                    default(AuthToken),
                    new Dictionary<string, string>()
                ));
        }
    }
}
