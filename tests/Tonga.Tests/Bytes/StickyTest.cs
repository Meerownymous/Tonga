using Tonga.Bytes;
using Tonga.IO;
using Xunit;

namespace Tonga.Tests.Bytes
{
    public sealed class StickyTest
    {
        [Fact]
        public void RemembersInput()
        {
            var calls = 0;

            var bytes = new global::Tonga.Bytes.Sticky(
                new BytesMorph(() =>
                {
                    ++calls;
                    return "";
                })
            );

            bytes.Raw();
            bytes.Raw();
            Assert.Equal(1, calls);
        }
    }
}
