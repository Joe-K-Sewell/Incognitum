using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IncognitumUnitTests.TestHelpers
{
    internal class TestHeaders : IEnumerable<KeyValuePair<String, IEnumerable<String>>>
    {
        public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
