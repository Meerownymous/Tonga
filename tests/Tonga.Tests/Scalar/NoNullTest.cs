using System.IO;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
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
