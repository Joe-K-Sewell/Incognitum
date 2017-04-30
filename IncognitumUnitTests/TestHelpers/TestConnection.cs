using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Incognitum.Connections;

namespace IncognitumUnitTests.TestHelpers
{
    internal sealed class TestConnection : Connection
    {
        internal TestConnection(Func<Request, Response> initialSendBehavior)
        {
            SendBehavior = initialSendBehavior;
        }

        internal Func<Request, Response> SendBehavior;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        internal override async Task<Response> SendAsync(Request request)
        {
            return SendBehavior(request);
        }
#pragma warning restore CS1998
    }
}
