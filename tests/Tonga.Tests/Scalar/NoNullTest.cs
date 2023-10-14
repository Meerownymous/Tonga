

using System.IO;
using Xunit;

namespace Tonga.Scalar.Tests
{
    public class NoNullTest
    {
        [Fact]
        public void RaisesError()
        {
            Assert.Throws<IOException>(
                () =>
                new NoNull<string>(null).Value());
        }

        [Fact]
        public void GivesFallback()
        {
            var fbk = "Here, take this instead";
            string val = null;
            Assert.True(
                new NoNull<string>(val, fbk).Value() == fbk,
                "can't get fallback value");
        }
    }
}
