

using Xunit;
using Tonga.IO;

namespace Tonga.Bytes.Tests
{
    public sealed class StickyTest
    {
        [Fact]
        public void RemembersInput()
        {
            var calls = 0;
            var bytes =
                new Sticky(() =>
                    new AsInput(() =>
                    {
                        ++calls;
                        return new AsInputStream("");
                    })
                );
            bytes.Bytes();
            bytes.Bytes();
            Assert.Equal(1, calls);
        }
    }
}
