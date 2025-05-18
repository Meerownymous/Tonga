using Tonga.Pipe;
using Xunit;

namespace Tonga.Tests.Pipe
{
    public sealed class AsPipeTest
    {
        [Fact]
        public void Yields()
        {
            Assert.Equal(
                1,
                AsPipe._<int, int>(
                    _ => 1
                ).Yield(2)
            );
        }

        [Fact]
        public void YieldsFromInput()
        {
            Assert.Equal(
                1,
                AsPipe._<int, int>(
                    ipt => ipt
                ).Yield(1)
            );
        }
    }
}
