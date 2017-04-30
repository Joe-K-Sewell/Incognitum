using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Incognitum;
using Incognitum.Connections;

namespace IncognitumUnitTests
{
    [TestClass]
    public class MastodonClientTests
    {
        [TestMethod]
        public void ctor_NullConnectionThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MastodonClient((IConnection) null));
        }

        [TestMethod]
        public void ctor_NullInstanceHostNameThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MastodonClient((String) null));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        public void ctor_WhitespaceInstanceHostNameThrowsException(String instanceHostName)
        {
            Assert.ThrowsException<ArgumentException>(() => new MastodonClient(instanceHostName));
        }
    }
}
