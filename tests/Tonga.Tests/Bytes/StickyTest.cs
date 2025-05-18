using Tonga.IO;
using Xunit;
using Sticky = Tonga.Bytes.Sticky;

namespace Tonga.Tests.Bytes
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
