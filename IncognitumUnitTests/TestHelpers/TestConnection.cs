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
        internal override async Task<Response> SendAsync(Request request)
        {
            return SendBehavior(request);
        }
    }
}
