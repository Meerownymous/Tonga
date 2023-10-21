

using Xunit;
using Tonga.IO;

namespace Tonga.Bytes.Tests
{
    public sealed class LiveBytesTest
    {
        [Fact]
        public void ReloadsInput()
        {
            var calls = 0;
            var bytes = new LiveBytes(() =>
                new InputOf(() =>
                {
                    ++calls;
                    return new InputStreamOf("");
                })
            );
            bytes.Bytes();
            bytes.Bytes();
            Assert.Equal(2, calls);
        }

        [Fact]
        public void ReloadsFunc()
        {
            var calls = 0;
            var bytes = new LiveBytes(
                () =>
                {
                    ++calls;
                    return new AsBytes(1);
                }
            );
            bytes.Bytes();
            bytes.Bytes();
            Assert.Equal(2, calls);
        }
    }
}
