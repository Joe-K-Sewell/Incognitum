using System;
using Xunit;

namespace IncognitumUnitTests
{
    public class Class1
    {
        [Theory]
        [InlineData("Yo")]
        public void BasicTest(String it)
        {
            var x = new Incognitum.Connections.HttpsConnection("mastodon.cloud");
        }
    }
}
