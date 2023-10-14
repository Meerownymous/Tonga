

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
            bytes.AsBytes();
            bytes.AsBytes();
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
                    return new BytesOf(1);
                }
            );
            bytes.AsBytes();
            bytes.AsBytes();
            Assert.Equal(2, calls);
        }
    }
}
