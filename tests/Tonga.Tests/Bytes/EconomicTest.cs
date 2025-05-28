using Tonga.IO;
using Xunit;
using Compiled = Tonga.Bytes.Compiled;

namespace Tonga.Tests.Bytes
{
    public sealed class EconomicTest
    {
        [Fact]
        public void RemembersInput()
        {
            var calls = 0;
            var bytes =
                new Compiled(() =>
                    new AsConduit(() =>
                    {
                        ++calls;
                        return new AsStream("");
                    })
                );
            bytes.Bytes();
            bytes.Bytes();
            Assert.Equal(1, calls);
        }
    }
}
