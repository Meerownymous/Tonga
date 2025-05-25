using System.IO;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public class NullRejectingTest
    {
        [Fact]
        public void RaisesError()
        {
            Assert.Throws<IOException>(
                () =>
                NullRejecting._<object>(null).Value());
        }

        [Fact]
        public void GivesFallback()
        {
            var fbk = "Here, take this instead";
            string val = null;
            Assert.Equal(
                fbk,
                NullRejecting._(val, fbk).Value()
            );
        }
    }
}
