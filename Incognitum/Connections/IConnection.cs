using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

using Incognitum.Authentication;

namespace Incognitum.Connections
{
    /// <summary>
    /// A connection to a particular Mastodon instance.
    /// </summary>
    public interface IConnection
    {
        Response Send(Request request);
    }
    
    /// <summary>
    /// A request sent to a Mastodon instance.
    /// </summary>
    public sealed class Request
    {
        public Verb Verb { get; private set; }
        public String Path { get; private set; }
        public AuthToken AuthToken { get; private set; }
        public Dictionary<String, String> Arguments { get; private set; }

        public Request(Verb verb, String path, AuthToken authToken, Dictionary<String, String> arguments)
        {
            Verb = verb;
            Path = path;
            AuthToken = authToken;
            Arguments = arguments;
        }
    }

    /// <summary>
    /// A kind of request that is sent to a Mastodon instance.
    /// </summary>
    public sealed class Verb
    {
        public static readonly Verb GetPublic = new Verb(HttpMethod.Get, false);
        public static readonly Verb Get = new Verb(HttpMethod.Get, true);
        public static readonly Verb Post = new Verb(HttpMethod.Post, true);
        public static readonly Verb Patch = new Verb(new HttpMethod("PATCH"), true);
        public static readonly Verb Delete = new Verb(HttpMethod.Delete, true);

        internal HttpMethod HttpMethod;
        internal Boolean RequireAuthToken;

        private Verb(HttpMethod httpMethod, Boolean requireAuthToken)
        {
            HttpMethod = httpMethod;
            RequireAuthToken = requireAuthToken;
        }
    }

    /// <summary>
    /// The response from a Mastodon instance to a <seealso cref="Request"/>.
    /// </summary>
    public sealed class Response
    {
        public String Content { get; private set; }
    }
}
