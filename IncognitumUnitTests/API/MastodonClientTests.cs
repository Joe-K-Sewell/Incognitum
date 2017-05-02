using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Incognitum.API;
using Incognitum.Connections;
using IncognitumUnitTests.TestHelpers;
using System.Linq;

namespace IncognitumUnitTests.API
{
    [TestClass]
    public class MastodonClientTests
    {
        [TestMethod]
        public void ConstructorConnection_NullThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MastodonClient((Connection) null));
        }

        [TestMethod]
        public void ConstructorString_NullThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MastodonClient((String) null));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        public void ConstructorString_WhitespaceOnlyThrowsException(String instanceHostName)
        {
            Assert.ThrowsException<ArgumentException>(() => new MastodonClient(instanceHostName));
        }

        [TestMethod]
        public void GetInstanceInformation_Nominal()
        {
            var connection = new TestConnection(req => 
            {
                Assert.AreEqual(Verb.GetPublic, req.Verb);
                Assert.AreEqual("/api/v1/instance", req.Path);
                Assert.AreEqual(null, req.AuthToken);
                Assert.AreEqual(0, req.Arguments.Count);

                return new Response(
                    @"{""uri"":""mastodon.social"",""title"":""Mastodon"",""description"":""The flagship Mastodon instance"",""email"":""eugen@zeonfederated.com"",""version"":""1.3.1""}",
                    new TestHeaders());
            });
            var client = new MastodonClient(connection);
            var instance = client.GetInstanceInformationAsync().Result;

            Assert.AreEqual("mastodon.social", instance.UriString);
            Assert.AreEqual(new Uri("https://mastodon.social/"), instance.Uri);
            Assert.AreEqual("Mastodon", instance.Title);
            Assert.AreEqual("The flagship Mastodon instance", instance.Description);
            Assert.AreEqual("eugen@zeonfederated.com", instance.Email);

            var extraData = instance.NonSpecData.Single();
            Assert.AreEqual("version", extraData.Key);
            Assert.AreEqual("1.3.1", extraData.Value);
        }

        [TestMethod]
        public void GetPublicTimelineAsync_Nominal()
        {
            var client = new MastodonClient("mastodon.social");
            var timelinePage = client.GetPublicTimelineAsync().Result;
        }
    }
}
