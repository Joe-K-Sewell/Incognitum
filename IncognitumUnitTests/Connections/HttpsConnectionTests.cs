using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Incognitum.Connections;
using System.Net.Http;

namespace IncognitumUnitTests
{
    [TestClass]
    public class HttpsConnectionTests
    {
        [TestMethod]
        public void ctor_NullHostnameThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new HttpsConnection(null));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        public void ctor_WhitespaceHostnameThrowsException(String hostName)
        {
            Assert.ThrowsException<ArgumentException>(() => new HttpsConnection(hostName));
        }

        [TestMethod]
        [DataRow("example.com")]
        [DataRow("mastodon.social")]
        [DataRow("mastodon.cloud")]
        [DataRow("mstdn.jp")]
        [DataRow("pawoo.net")]
        [DataRow("mastodon.xyz")]
        [DataRow("social.tchncs.de")]
        public void ctor_DnsName(String hostName)
        {
            var connection = new HttpsConnection(hostName);
            Assert.AreEqual($"https://{hostName}/", connection.InstanceUri.ToString());
        }

        [TestMethod]
        [DataRow("127.0.0.1")]
        [DataRow("8.8.8.8")]
        public void ctor_IpV4(String ipAddress)
        {
            var connection = new HttpsConnection(ipAddress);
            Assert.AreEqual($"https://{ipAddress}/", connection.InstanceUri.ToString());
        }

        [TestMethod]
        [DataRow("2001:0db8:85a3:0000:0000:8a2e:0370:7334", "[2001:db8:85a3::8a2e:370:7334]")]
        [DataRow("2001:db8:85a3:0:0:8a2e:370:7334", "[2001:db8:85a3::8a2e:370:7334]")]
        [DataRow("2001:db8:85a3::8a2e:370:7334", "[2001:db8:85a3::8a2e:370:7334]")]
        [DataRow("0:0:0:0:0:0:0:1", "[::1]")]
        [DataRow("::1", "[::1]")]
        [DataRow("::", "[::]")]
        public void ctor_IpV6(String ipAddress, String uriRepresentation)
        {
            var connection = new HttpsConnection(ipAddress);
            Assert.AreEqual($"https://{uriRepresentation}/", connection.InstanceUri.ToString());

            var connectionWithBraces = new HttpsConnection($"[{ipAddress}]");
            Assert.AreEqual($"https://{uriRepresentation}/", connectionWithBraces.InstanceUri.ToString());
        }

        [TestMethod]
        [DataRow("example.com")]
        [DataRow("mastodon.social")]
        [DataRow("mastodon.cloud")]
        public void ctor_WithTrailingSlash(String hostName)
        {
            var connection = new HttpsConnection(hostName + "/");
            Assert.AreEqual($"https://{hostName}/", connection.InstanceUri.ToString());
        }

        [TestMethod]
        [DataRow("example.com")]
        [DataRow("mastodon.social")]
        [DataRow("mastodon.cloud")]
        [DataRow("127.0.0.1")]
        [DataRow("[2001:db8:85a3:0:0:8a2e:370:7334]")]
        public void ctor_WithPath(String hostName)
        {
            Assert.ThrowsException<ArgumentException>(() => new HttpsConnection(hostName + "/api"));
        }
    }
}
