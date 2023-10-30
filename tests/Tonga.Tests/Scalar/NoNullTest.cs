

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
                NoNull._<object>(null).Value());
        }

        [Fact]
        public void GivesFallback()
        {
            var fbk = "Here, take this instead";
            string val = null;
            Assert.Equal(
                fbk,
                NoNull._(val, fbk).Value()
            );
        }
    }
}
